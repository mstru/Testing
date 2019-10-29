namespace Testing.Automation.API
{
    using System;
    using System.Net.Http;
    using Builders.Contracts.HttpRequests;
    using Builders.Contracts.Routes;

    /// <summary>
    /// Used for building a route test.
    /// </summary>
    public interface IRouteTestBuilder
    {
        /// <summary>
        /// Sets the route location to test.
        /// </summary>
        /// <param name="location">Location as string.</param>
        /// <returns>Route test builder.</returns>
        IShouldMapTestBuilder ShouldMap(string location);

        /// <summary>
        /// Sets the route location to test.
        /// </summary>
        /// <param name="location">Location as Uri.</param>
        /// <returns>Route test builder.</returns>
        IShouldMapTestBuilder ShouldMap(Uri location);

        /// <summary>
        /// Sets the route HTTP request message to test.
        /// </summary>
        /// <param name="requestMessage">Instance of type HttpRequestMessage.</param>
        /// <returns>Route test builder.</returns>
        IShouldMapTestBuilder ShouldMap(HttpRequestMessage requestMessage);

        /// <summary>
        /// Sets the route HTTP request message to test using a builder.
        /// </summary>
        /// <param name="httpRequestMessageBuilder">Builder for HTTP request message.</param>
        /// <returns>Route test builder.</returns>
        IShouldMapTestBuilder ShouldMap(Action<IHttpRequestMessageBuilder> httpRequestMessageBuilder);
    }
}
