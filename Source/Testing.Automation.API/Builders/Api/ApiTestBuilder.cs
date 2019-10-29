namespace Testing.Automation.API.Builders.Api
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net.Http;
    using Common.Extensions;
    using Contracts.HttpRequests;
    using Contracts.HttpResponseMessages;
    using Contracts.Servers;
    using HttpMessages;
    using Common.Servers;
    using System.Threading;
    using global::Testing.Automation.Reporter.Exceptions;

    /// <summary>
    /// Poskytuje možnosti nastavenia požiadavky HTTP a otestovanie odpovede HTTP.
    /// </summary>
    public class ApiTestBuilder : IApiBuilder, IServerTestBuilder
    {
        private readonly HttpClient client;
        private readonly bool transformRequest;
        private readonly bool disposeServer;
        private readonly IDisposable server;

        private HttpRequestMessage httpRequestMessage { get; set; }
        public HttpResponseMessage httpResponseMessage { get; set; }

        /// <summary>
        /// Inicializacia novej instancie <see cref="ApiTestBuilder" /> triedy.
        /// </summary>
        /// <param name="client">Inicializacia HTTP správy na odoslanie požiadavky.</param>
        /// <param name="transformRequest">Označuje, či sa má transformovať vzhľadom na falošný URI absolútnej žiadosti.</param>
        /// <param name="disposeServer">Označuje, či sa má server a klient zlikvidovať po dokončení testu</param>
        /// <param name="server">ID server, ktorý sa má použiť pre žiadosť.</param>
        public ApiTestBuilder(
            HttpClient client,
            bool transformRequest = false,
            bool disposeServer = false,
            IDisposable server = null)
        {
            this.client = client;
            this.transformRequest = transformRequest;
            this.disposeServer = disposeServer;
            this.server = server;
        }

        /// <summary>
        /// Pridáva predvolenú hlavičku pre každé volanie.
        /// </summary>
        /// <param name="name">Názov hlavičky.</param>
        /// <param name="value">Hodnota hlavičky.</param>
        public IApiBuilder WithDefaultRequestHeader(string name, string value)
        {
            this.client.DefaultRequestHeaders.Add(name, value);
            return this;
        }

        /// <summary>
        /// Pridáva predvolenú hlavičku pre každé volanie.
        /// </summary>
        /// <param name="name">Názov hlavičky.</param>
        /// <param name="value">Hodnota hlavičky.</param>
        public IApiBuilder WithDefaultRequestHeader(string name, IEnumerable<string> values)
        {
            this.client.DefaultRequestHeaders.Add(name, values);
            return this;
        }

        /// <summary>
        /// Pridáva predvolenú hlavičku pre každé volanie.
        /// </summary>
        /// <param name="name">Názov hlavičky.</param>
        /// <param name="value">Hodnota hlavičky.</param>
        public IApiBuilder WithDefaultRequestHeaders(IDictionary<string, IEnumerable<string>> headers)
        {
            headers.ForEach(h => this.WithDefaultRequestHeader(h.Key, h.Value));
            return this;
        }

        /// <summary>
        /// Odstráni predtým pridanú predvolenú hlavičku z každej požiadavky.
        /// </summary>
        /// <param name="name">Názov hlavičky.</param>
        public IApiBuilder WithoutDefaultRequestHeader(string name)
        {
            if (client.DefaultRequestHeaders.Contains(name))
            {
                client.DefaultRequestHeaders.Remove(name);
            }

            return this;
        }

        /// <summary>
        /// Prida poziadavku HTTP s autorizaciou
        /// </summary>
        /// <param name="requestMessage">Instance of HttpRequestMessage.</param>
        public IServerTestBuilder WithHttpRequestMessage(HttpRequestMessage requestMessage)
        {
            this.httpRequestMessage = requestMessage;
            this.AddToken();
            this.AddContentType();
            if (this.transformRequest)
            {
                this.TransformRelativeRequestUri();
            }

            return this;
        }

        /// <summary>
        /// Prida poziadavku HTTP bez autorizaciou
        /// </summary>
        /// <param name="requestMessage">Instance of HttpRequestMessage.</param>
        public IServerTestBuilder WithHttpRequestMessageUnauthorized(HttpRequestMessage requestMessage)
        {
            this.httpRequestMessage = requestMessage;
            if (this.transformRequest)
            {
                this.TransformRelativeRequestUri();
            }

            return this;
        }

        /// <summary>
        /// Prida poziadavku HTTP s autorizaciou
        /// </summary>
        /// <param name="httpRequestMessageBuilder">Builder for HTTP request message.</param>
        /// <returns>Server test builder to test the returned HTTP response.</returns>
        public IServerTestBuilder WithHttpRequestMessage(Action<IHttpRequestMessageBuilder> httpRequestMessageBuilder)
        {
            var httpBuilder = new HttpRequestMessageBuilder();

            httpRequestMessageBuilder(httpBuilder);
            return this.WithHttpRequestMessage(httpBuilder.GetHttpRequestMessage());
        }

        /// <summary>
        /// Prida poziadavku HTTP bez autorizaciou
        /// </summary>
        /// <param name="httpRequestMessageBuilder">Builder for HTTP request message.</param>
        /// <returns>Server test builder to test the returned HTTP response.</returns>
        public IServerTestBuilder WithHttpRequestMessageUnauthorized(Action<IHttpRequestMessageBuilder> httpRequestMessageBuilder)
        {
            var httpBuilder = new HttpRequestMessageBuilder();

            httpRequestMessageBuilder(httpBuilder);
            return this.WithHttpRequestMessageUnauthorized(httpBuilder.GetHttpRequestMessage());
        }

        /// <summary>
        /// Testuje konkrétnu odpoveď HTTP
        /// </summary>
        /// <returns>Nástroj na testovanie správy reakcie protokolu HTTP.</returns>
        public IHttpHandlerResponseMessageWithTimeTestBuilder ShouldReturnHttpResponseMessage()
        {
            var clients = new HttpClient();
           
            var serverHandler = new ServerHttpMessageHandler(clients, this.disposeServer);
            using (var invoker = new HttpMessageInvoker(serverHandler, true))
            {
                var stopwatch = Stopwatch.StartNew();

                string uriHrefToString = string.Format("{0}", httpRequestMessage.RequestUri.ToString());
                //Reporter.Info("Uri: " + uriHrefToString, CodeBlockType.Label); 

                try
                {
                    httpResponseMessage = invoker.SendAsync(this.httpRequestMessage, CancellationToken.None).Result;
                }
                catch (Exception ex)
                {

                    this.ThrowNewHttpResponseMessageAssertionException(string.Format("{0}{1}{1}{2}", uriHrefToString, Environment.NewLine, ex.ToString()));
                }
    
                stopwatch.Stop();
   
                if (this.disposeServer && this.server != null)
                {
                    this.server.Dispose();
                }
                return new HttpHandlerResponseMessageWithTimeTestBuilder(httpResponseMessage, stopwatch.Elapsed);
            }
        }



        private void TransformRelativeRequestUri()
        {
            if (this.httpRequestMessage.RequestUri != null && !this.httpRequestMessage.RequestUri.IsAbsoluteUri)
            {
                this.httpRequestMessage.TransformToAbsoluteRequestUri();
            }            
        }

        private void AddToken()
        {
            if (this.httpRequestMessage.Headers.Authorization == null)
            {
                this.httpRequestMessage.Headers.Add(HttpHeader.Authorization, MyWebApi.JwtToken);
            }
        }

        private void AddContentType()
        {
            if (this.httpRequestMessage.Headers == null)
            {
                this.httpRequestMessage.Headers.Add("Content-Type", MyWebApi.ContentType);
            }
        }

        /// <summary>
        /// Chybova hlaska.
        /// </summary>
        /// <param name="formatException">Exception.</param>
        protected void ThrowNewHttpResponseMessageAssertionException(string formatException)
        {
            throw new VerificationException($"When testing Api, one or more errors occurred. {formatException}");
        }  
    }
}