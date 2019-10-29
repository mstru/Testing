namespace Testing.Automation.API.Builders.HttpMessages
{
    using System;
    using System.Net.Http;
    using Base;
    using Contracts.Handlers;

    /// <summary>
    /// Používa sa na pridávanie vnútorných obslužných programov správ HTTP.
    /// </summary>
    public class InnerHttpMessageHandlerBuilder : BaseHandlerTestBuilder,
        IInnerHttpMessageHandlerBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InnerHttpMessageHandlerBuilder" /> class.
        /// </summary>
        /// <param name="handler">HTTP message handler on which the inner handler will be set.</param>
        public InnerHttpMessageHandlerBuilder()
            : base()
        {
        }

        /// <summary>
        /// Nastaví vnútorný handler HTTP na aktuálny handler, ak je typu DelegatingHandler.
        /// </summary>
        /// <typeparam name="TInnerHandler">Inner HttpMessageHandler type to set.</typeparam>
        public void WithInnerHandler<TInnerHandler>()
            where TInnerHandler : HttpMessageHandler, new()
        {
            this.WithInnerHandler(new TInnerHandler());
        }

        /// <summary>
        /// Nastaví poskytnutú inštanciu ako vnútorný handler HTTP na aktuálny handler, ak je typu DelegatingHandler.
        /// </summary>
        /// <typeparam name="TInnerHandler">Inner HttpMessageHandler type to set.</typeparam>
        /// <param name="innerHandler">Instance of type HttpMessageHandler.</param>
        public void WithInnerHandler<TInnerHandler>(TInnerHandler innerHandler)
            where TInnerHandler : HttpMessageHandler
        {
            this.SetInnerHandler(innerHandler);
        }

        /// <summary>
        /// Nastaví vnútorný handler HTTP pomocou konštrukčnej funkcie na aktuálny handler, ak je typu DelegatingHandler. 
        /// </summary>
        /// <typeparam name="TInnerHandler">Inner HttpMessageHandler type to set.</typeparam>
        /// <param name="construction">Construction function returning the instantiated inner HttpMessageHandler.</param>
        public void WithInnerHandler<TInnerHandler>(Func<TInnerHandler> construction)
            where TInnerHandler : HttpMessageHandler
        {
            var innerHandlerInstance = construction();
            this.WithInnerHandler(innerHandlerInstance);
        }

        /// <summary>
        /// Nastaví vnútorný handler HTTP pomocou nástroja Builder k aktuálnemu psovodu, ak je typu DelegatingHandler.
        /// </summary>
        /// <typeparam name="TInnerHandler">Inner HttpMessageHandler type to set.</typeparam>
        /// <param name="httpMessageHandlerBuilder">Inner HttpMessageHandler builder.</param>
        public void WithInnerHandler<TInnerHandler>(Action<IInnerHttpMessageHandlerBuilder> httpMessageHandlerBuilder)
            where TInnerHandler : HttpMessageHandler, new()
        {
            var newHttpMessageHandlerBuilder = new InnerHttpMessageHandlerBuilder();
            this.WithInnerHandler(newHttpMessageHandlerBuilder.Handler);
        }
    }
}
