namespace Testing.Automation.API.Builders.HttpMessages
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Web.Http;
    using Base;
    using Contracts.Handlers;
    using Contracts.HttpRequests;
    using Contracts.HttpResponseMessages;
    using Utilities.Validators;

    /// <summary>
    /// Používa sa na testovanie obslužných programov správ HTTP.
    /// </summary>
    public class HttpMessageHandlerTestBuilder
        : BaseHandlerTestBuilder, IHttpMessageHandlerBuilder, IHttpMessageHandlerTestBuilder
    {
        private HttpRequestMessage httpRequestMessage;
        private HttpConfiguration httpConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponseMessageTestBuilder" /> class.
        /// </summary>
        /// <param name="handler">HTTP handler which will be tested.</param>
        public HttpMessageHandlerTestBuilder()
            : base()
        {
        }

        private HttpConfiguration HttpConfiguration
        {
            get { return this.httpConfiguration ?? MyWebApi.Configuration ?? new HttpConfiguration(); }
        }

        /// <summary>
        /// Nastaví vnútorný handler HTTP na aktuálny handler, ak je typu DelegatingHandler.
        /// </summary>
        /// <typeparam name="TInnerHandler">Inner HttpMessageHandler type to set.</typeparam>
        /// <returns>The same HTTP handler builder.</returns>
        public IHttpMessageHandlerBuilder WithInnerHandler<TInnerHandler>()
            where TInnerHandler : HttpMessageHandler, new()
        {
            return this.WithInnerHandler(new TInnerHandler());
        }

        /// <summary>
        /// Nastaví poskytnutú inštanciu ako vnútorný handler HTTP na aktuálny handler, ak je typu DelegatingHandler. 
        /// </summary>
        /// <typeparam name="TInnerHandler">Inner HttpMessageHandler type to set.</typeparam>
        /// <param name="innerHandler">Instance of type HttpMessageHandler.</param>
        /// <returns>The same HTTP handler builder.</returns>
        public IHttpMessageHandlerBuilder WithInnerHandler<TInnerHandler>(TInnerHandler innerHandler)
            where TInnerHandler : HttpMessageHandler
        {
            this.SetInnerHandler(innerHandler);
            return this;
        }

        /// <summary>
        /// Nastaví vnútorný handler HTTP pomocou konštrukčnej funkcie na aktuálny handler, ak je typu DelegatingHandler. 
        /// </summary>
        /// <typeparam name="TInnerHandler">Inner HttpMessageHandler type to set.</typeparam>
        /// <param name="construction">Construction function returning the instantiated inner HttpMessageHandler.</param>
        /// <returns>The same HTTP handler builder.</returns>
        public IHttpMessageHandlerBuilder WithInnerHandler<TInnerHandler>(Func<TInnerHandler> construction)
            where TInnerHandler : HttpMessageHandler
        {
            var innerHandlerInstance = construction();
            return this.WithInnerHandler(innerHandlerInstance);
        }

        /// <summary>
        /// Nastaví vnútorný handler HTTP pomocou nástroja Builder k aktuálnemu pripadu, ak je typu DelegatingHandler. 
        /// </summary>
        /// <typeparam name="TInnerHandler">Inner HttpMessageHandler type to set.</typeparam>
        /// <param name="httpMessageHandlerBuilder">Inner HttpMessageHandler builder.</param>
        /// <returns>The same HTTP handler builder.</returns>
        public IHttpMessageHandlerBuilder WithInnerHandler<TInnerHandler>(
            Action<IInnerHttpMessageHandlerBuilder> httpMessageHandlerBuilder)
            where TInnerHandler : HttpMessageHandler, new()
        {
            var newHttpMessageHandlerBuilder = new InnerHttpMessageHandlerBuilder();
            return this.WithInnerHandler(newHttpMessageHandlerBuilder.AndProvideTheHandler());
        }

        /// <summary>
        /// Nastaví konfiguráciu protokolu HTTP pre aktuálny testovací prípad.
        /// </summary>
        /// <param name="config">Instance of HttpConfiguration.</param>
        /// <returns>The same HTTP handler builder.</returns>
        public IHttpMessageHandlerBuilder WithHttpConfiguration(HttpConfiguration config)
        {
            this.httpConfiguration = config;
            return this;
        }

        /// <summary>
        /// Pridá hlásenie žiadosti HTTP pre aktuálny testovací prípad.
        /// </summary>
        /// <param name="requestMessage">Instance of HttpRequestMessage.</param>
        /// <returns>The same HTTP handler builder.</returns>
        public IHttpMessageHandlerTestBuilder WithHttpRequestMessage(HttpRequestMessage requestMessage)
        {
            this.httpRequestMessage = requestMessage;
            var configuration = this.HttpConfiguration;
            this.httpRequestMessage.SetConfiguration(configuration);
            return this;
        }

        /// <summary>
        /// Nastaví konfiguráciu protokolu HTTP pre aktuálny testovací prípad..
        /// </summary>
        /// <param name="httpRequestMessageBuilder">Builder for HTTP request message.</param>
        /// <returns>The same HTTP handler builder.</returns>
        public IHttpMessageHandlerTestBuilder WithHttpRequestMessage(Action<IHttpRequestMessageBuilder> httpRequestMessageBuilder)
        {
            var httpBuilder = new HttpRequestMessageBuilder();
            httpRequestMessageBuilder(httpBuilder);
            return this.WithHttpRequestMessage(httpBuilder.GetHttpRequestMessage());
        }

        /// <summary>
        /// Testuje handler HTTP na úspešné vrátenie správy HTTP.
        /// </summary>
        /// <returns>HTTP response message test builder.</returns>
        public IHttpHandlerResponseMessageTestBuilder ShouldReturnHttpResponseMessage()
        {
            HttpResponseMessage httpResponseMessage = null;
            using (var httpMessageInvoker = new HttpMessageInvoker(this.Handler))
            {
                try
                {
                    httpResponseMessage = httpMessageInvoker.SendAsync(this.httpRequestMessage, CancellationToken.None).Result;
                }
                catch (Exception exception)
                {
                    CommonValidator.CheckForException(exception);
                }
            }

            return new HttpHandlerResponseMessageTestBuilder(
                httpResponseMessage);
        }

        /// <summary>
        /// Získanie správy o požiadavke HTTP, ktorá sa používa pri testovaní obsluhy.
        /// </summary>
        /// <returns>Instance of HttpRequestMessage.</returns>
        public HttpRequestMessage AndProvideTheHttpRequestMessage()
        {
            return this.httpRequestMessage;
        }

        /// <summary>
        /// Získa konfiguráciu protokolu HTTP použitú pri testovaní obsluhy.
        /// </summary>
        /// <returns>Instance of HttpConfiguration.</returns>
        public HttpConfiguration AndProvideTheHttpConfiguration()
        {
            return this.HttpConfiguration;
        }
    }
}
