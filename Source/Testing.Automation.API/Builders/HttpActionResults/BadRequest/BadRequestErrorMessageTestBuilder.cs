﻿namespace Testing.Automation.API.Builders.HttpActionResults.BadRequest
{
    using System;
    using System.Web.Http;
    using Base;
    using Common.Extensions;
    using Contracts.Base;
    using Contracts.HttpActionResults.BadRequest;
    using global::Testing.Automation.Reporter.Exceptions;

    /// <summary>
    /// Used for testing specific bad request error messages.
    /// </summary>
    public class BadRequestErrorMessageTestBuilder
        : BaseTestBuilderWithCaughtException, IBadRequestErrorMessageTestBuilder
    {
        private readonly string actualMessage;

        /// <summary>
        /// Initializes a new instance of the <see cref="BadRequestErrorMessageTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="actualMessage">Actual error message received from bad request result.</param>
        public BadRequestErrorMessageTestBuilder(
            ApiController controller,
            string actionName,
            Exception caughtException,
            string actualMessage)
            : base(controller, actionName, caughtException)
        {
            this.actualMessage = actualMessage;
        }

        /// <summary>
        /// Tests whether particular error message is equal to given message.
        /// </summary>
        /// <param name="errorMessage">Expected error message for particular key.</param>
        /// <returns>Base test builder.</returns>
        public IBaseTestBuilderWithCaughtException ThatEquals(string errorMessage)
        {
            if (this.actualMessage != errorMessage)
            {
                this.ThrowNewBadRequestResultAssertionException(
                    "When calling {0} action in {1} expected bad request error message to be '{2}', but instead found '{3}'.",
                    errorMessage);
            }

            return this.NewAndProvideTestBuilder();
        }

        /// <summary>
        /// Tests whether particular error message begins with given message.
        /// </summary>
        /// <param name="beginMessage">Expected beginning for particular error message.</param>
        /// <returns>Base test builder.</returns>
        public IBaseTestBuilderWithCaughtException BeginningWith(string beginMessage)
        {
            if (!this.actualMessage.StartsWith(beginMessage))
            {
                this.ThrowNewBadRequestResultAssertionException(
                    "When calling {0} action in {1} expected bad request error message to begin with '{2}', but instead found '{3}'.",
                    beginMessage);
            }

            return this.NewAndProvideTestBuilder();
        }

        /// <summary>
        /// Tests whether particular error message ends with given message.
        /// </summary>
        /// <param name="endMessage">Expected ending for particular error message.</param>
        /// <returns>Base test builder.</returns>
        public IBaseTestBuilderWithCaughtException EndingWith(string endMessage)
        {
            if (!this.actualMessage.EndsWith(endMessage))
            {
                this.ThrowNewBadRequestResultAssertionException(
                    "When calling {0} action in {1} expected bad request error message to end with '{2}', but instead found '{3}'.",
                    endMessage);
            }

            return this.NewAndProvideTestBuilder();
        }

        /// <summary>
        /// Tests whether particular error message contains given message.
        /// </summary>
        /// <param name="containsMessage">Expected containing string for particular error message.</param>
        /// <returns>Base test builder.</returns>
        public IBaseTestBuilderWithCaughtException Containing(string containsMessage)
        {
            if (!this.actualMessage.Contains(containsMessage))
            {
                this.ThrowNewBadRequestResultAssertionException(
                    "When calling {0} action in {1} expected bad request error message to contain '{2}', but instead found '{3}'.",
                    containsMessage);
            }

            return this.NewAndProvideTestBuilder();
        }

        private void ThrowNewBadRequestResultAssertionException(string messageFormat, string operation)
        {
            throw new BadRequestResultAssertionException(string.Format(
                messageFormat,
                this.ActionName,
                this.Controller.GetName(),
                operation,
                this.actualMessage));
        }
    }
}
