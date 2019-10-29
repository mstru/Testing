﻿namespace Testing.Automation.API.Builders.Contracts.Handlers
{
    using System.Net.Http;
    using Base;
    using HttpResponseMessages;

    /// <summary>
    /// Used for testing HTTP message handlers.
    /// </summary>
    public interface IHttpMessageHandlerTestBuilder : IBaseHandlerTestBuilder
    {
        /// <summary>
        /// Tests the HTTP handler for returning HTTP response message successfully.
        /// </summary>
        /// <returns>HTTP response message test builder.</returns>
        IHttpHandlerResponseMessageTestBuilder ShouldReturnHttpResponseMessage();

        /// <summary>
        /// Gets the HTTP request message used in the testing.
        /// </summary>
        /// <returns>Instance of HttpRequestMessage.</returns>
        HttpRequestMessage AndProvideTheHttpRequestMessage();
    }
}
