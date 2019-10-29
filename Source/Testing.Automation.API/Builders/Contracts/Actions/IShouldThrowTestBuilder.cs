﻿namespace Testing.Automation.API.Builders.Contracts.Actions
{
    using Base;
    using ExceptionErrors;

    /// <summary>
    /// Used for testing whether action throws exception.
    /// </summary>
    public interface IShouldThrowTestBuilder : IBaseTestBuilderWithCaughtException
    {
        /// <summary>
        /// Tests whether action throws any exception.
        /// </summary>
        /// <returns>Exception test builder.</returns>
        IExceptionTestBuilder Exception();

        /// <summary>
        /// Tests whether action throws any AggregateException.
        /// </summary>
        /// <param name="withNumberOfInnerExceptions">Optional expected number of total inner exceptions.</param>
        /// <returns>AggregateException test builder.</returns>
        IAggregateExceptionTestBuilder AggregateException(int? withNumberOfInnerExceptions = null);

        /// <summary>
        /// Tests whether action throws any HttpResponseException.
        /// </summary>
        /// <returns>HttpResponseException test builder.</returns>
        IHttpResponseExceptionTestBuilder HttpResponseException();
    }
}
