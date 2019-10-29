namespace Testing.Automation.API
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using Builders.Contracts.HttpRequests;
    using Builders.Contracts.Servers;

    /// <summary>
    /// Poskytuje možnosti nastavenia požiadavky HTTP na testovanie.
    /// </summary>
    public interface IApiBuilder
    {
        /// <summary>
        /// Pridáva predvolenú hlavičku pre každu žiadosti testovanej na serveri.
        /// </summary>
        /// <param name="name">Name of the header.</param>
        /// <param name="value">Value of the header.</param>
        IApiBuilder WithDefaultRequestHeader(string name, string value);

        /// <summary>
        /// Pridáva predvolenú hlavičku pre každu žiadosti.
        /// </summary>
        /// <param name="name">Name of the header.</param>
        /// <param name="values">Collection of values for the header.</param>
        IApiBuilder WithDefaultRequestHeader(string name, IEnumerable<string> values);

        /// <summary>
        /// Pridáva predvolenú hlavičku pre každu žiadosti testovanej na serveri.
        /// </summary>
        /// <param name="headers">Dictionary of headers to add.</param>
        IApiBuilder WithDefaultRequestHeaders(IDictionary<string, IEnumerable<string>> headers);

        /// <summary>
        /// Odstráni predtým pridanú predvolenú hlavičku z každej ziadosti testovanej na serveri.
        /// </summary>
        /// <param name="name">Name of the header.</param>
        IApiBuilder WithoutDefaultRequestHeader(string name);

        /// <summary>
        /// Pridá požiadavku HTTP na testovaný server.
        /// </summary>
        /// <param name="requestMessage">Instance of HttpRequestMessage.</param>
        IServerTestBuilder WithHttpRequestMessage(HttpRequestMessage requestMessage);

        /// <summary>
        /// Pridá požiadavku HTTP na testovaný server.
        /// </summary>
        /// <param name="httpRequestMessageBuilder">Builder for HTTP request message.</param>
        IServerTestBuilder WithHttpRequestMessage(Action<IHttpRequestMessageBuilder> httpRequestMessageBuilder);

        /// <summary>
        /// Pridá požiadavku HTTP na testovaný server bez autorizacie
        /// </summary>
        /// <param name="httpRequestMessageBuilder">Builder for HTTP request message.</param>
        IServerTestBuilder WithHttpRequestMessageUnauthorized(Action<IHttpRequestMessageBuilder> httpRequestMessageBuilder);
    }
}
