namespace Testing.Automation.API.Builders.Models
{
    using System;
    using Base;
    using Contracts.HttpResponseMessages;
    using Contracts.Models;
    using global::Testing.Automation.Reporter.Exceptions;
    using Utilities;

    /// <summary>
    /// Used for testing HTTP response message content model from HTTP message handler.
    /// </summary>
    /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
    public class HttpHandlerModelDetailsTestBuilder<TResponseModel>
        : BaseHandlerTestBuilder, IHttpHandlerModelDetailsTestBuilder<TResponseModel>
    {
        private readonly IHttpHandlerResponseMessageTestBuilder httpHandlerResponseMessageTestBuilder;
        private readonly TResponseModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpHandlerModelDetailsTestBuilder{TResponseModel}" /> class.
        /// </summary>
        /// <param name="handler">The HTTP message handler, from which the response model is collected.</param>
        /// <param name="httpHandlerResponseMessageTestBuilder">The original HTTP handler response message test builder.</param>
        /// <param name="model">The response model to test.</param>
        public HttpHandlerModelDetailsTestBuilder(
            IHttpHandlerResponseMessageTestBuilder httpHandlerResponseMessageTestBuilder,
            TResponseModel model = default(TResponseModel))
            : base()
        {
            this.httpHandlerResponseMessageTestBuilder = httpHandlerResponseMessageTestBuilder;
            this.model = model;
        }

        public HttpHandlerModelDetailsTestBuilder()
            : base()
        {

        }

        /// <summary>
        /// Tests whether the returned response model from the HTTP response message passes given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions on the response model.</param>
        /// <returns>Builder for testing the HTTP response message.</returns>
        public IHttpHandlerResponseMessageTestBuilder Passing(Action<TResponseModel> assertions)
        {
            assertions(this.model);
            return this.httpHandlerResponseMessageTestBuilder;
        }

        /// <summary>
        /// Tests whether the returned response model from the HTTP response message passes given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the response model.</param>
        /// <returns>Builder for testing the HTTP response message.</returns>
        public IHttpHandlerResponseMessageTestBuilder Passing(Func<TResponseModel, bool> predicate)
        {
            if (!predicate(this.model))
            {
                throw new ResponseModelAssertionException(string.Format(
                            "When testing, expected HTTP response message content model {0} to pass the given condition, but it failed.",
                            typeof(TResponseModel).ToFriendlyTypeName()));
            }

            return this.httpHandlerResponseMessageTestBuilder;
        }

        /// <summary>
        /// Gets the HTTP response message content model used in the testing.
        /// </summary>
        /// <returns>Instance of the content model type.</returns>
        public TResponseModel AndProvideTheModel()
        {
            return this.model;
        }
    }
}
