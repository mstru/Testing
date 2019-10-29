namespace Testing.Automation.API.Builders.Base
{
    using System.Net.Http;
    using Common.Extensions;
    using Contracts.Base;
    using global::Testing.Automation.Reporter.Exceptions;

    /// <summary>
    /// Zakladna trieda pre test builders.
    /// </summary>
    public abstract class BaseHandlerTestBuilder : IBaseHandlerTestBuilder
    {
        /// <summary>
        /// Ziskanie HTTP message pouziteho pri testovani.
        /// </summary>
        /// <value>Instance of HttpMessageHandler.</value>
        protected HttpMessageHandler Handler { get; private set; }

        /// <summary>
        /// Ziskanie HTTP message pouziteho pri testovani.
        /// </summary>
        /// <returns>Instance of HttpMessageHandler.</returns>
        public HttpMessageHandler AndProvideTheHandler()
        {
            return this.Handler;
        }

        /// <summary>
        /// Sets inner HTTP message handler to the current message handler.
        /// </summary>
        /// <param name="innerHandler">Instance of HttpMessageHandler.</param>
        protected void SetInnerHandler(HttpMessageHandler innerHandler)
        {
            var handlerAsDelegatingHandler = this.Handler as DelegatingHandler;
            if (handlerAsDelegatingHandler == null)
            {
                throw new HttpHandlerAssertionException(string.Format(
                    "When adding inner handler {0} to {1}, expected {1} to be DelegatingHandler, but in fact was not.",
                    innerHandler.GetName(),
                    this.Handler.GetName()));
            }

            handlerAsDelegatingHandler.InnerHandler = innerHandler;
        }
    }
}
