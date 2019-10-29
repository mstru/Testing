namespace Testing.Automation.API.Builders.Actions
{
    using System;
    using System.Web.Http;
    using Base;
    using Common.Extensions;
    using Contracts.Actions;
    using Contracts.ExceptionErrors;
    using ExceptionErrors;
    using global::Testing.Automation.Reporter.Exceptions;

    /// <summary>
    /// Používa sa na testovanie toho, či akcia udeľuje výnimku.
    /// </summary>
    public class ShouldThrowTestBuilder : BaseTestBuilderWithCaughtException, IShouldThrowTestBuilder
    {
        private readonly IExceptionTestBuilder exceptionTestBuilder;

        /// <summary>
        /// Inicializacia novej instancie <see cref="ShouldThrowTestBuilder" /> triedy.
        /// </summary>
        /// <param name="controller">Controller na ktorom bude cinnost testovana.</param>
        /// <param name="actionName">Nazov testovanej akcie.</param>
        /// <param name="caughtException">Chyba vynimky.</param>
        public ShouldThrowTestBuilder(
            ApiController controller,
            string actionName,
            Exception caughtException)
            : base(controller, actionName, caughtException)
        {
            this.exceptionTestBuilder = new ExceptionTestBuilder(this.Controller, this.ActionName, this.CaughtException);
        }

        /// <summary>
        /// Testuje, či akcia uvádza akúkoľvek výnimku.
        /// </summary>
        /// <returns>Exception test builder.</returns>
        public IExceptionTestBuilder Exception()
        {
            return this.exceptionTestBuilder;
        }

        /// <summary>
        /// Testuje, či sa akcia hodí akémukoľvek AggregateException.
        /// </summary>
        /// <param name="withNumberOfInnerExceptions">Volitelne nastavenie pre celkovy pocet vynimiek.</param>
        /// <returns>AggregateException test builder.</returns>
        public IAggregateExceptionTestBuilder AggregateException(int? withNumberOfInnerExceptions = null)
        {
            this.exceptionTestBuilder.OfType<AggregateException>();
            var aggregateException = this.CaughtException as AggregateException;
            var innerExceptionsCount = aggregateException.InnerExceptions.Count;
            if (withNumberOfInnerExceptions.HasValue &&
                withNumberOfInnerExceptions != innerExceptionsCount)
            {
                throw new InvalidExceptionAssertionException(string.Format(
                    "When calling {0} action in {1} expected AggregateException to contain {2} inner exceptions, but in fact contained {3}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    withNumberOfInnerExceptions,
                    innerExceptionsCount));
            }

            return new AggregateExceptionTestBuilder(
                this.Controller,
                this.ActionName,
                aggregateException);
        }

        /// <summary>
        /// Testuje, či akcia hodí akúkoľvek HttpResponseException.
        /// </summary>
        /// <returns>HttpResponseException test builder.</returns>
        public IHttpResponseExceptionTestBuilder HttpResponseException()
        {
            this.exceptionTestBuilder.OfType<HttpResponseException>();
            return new HttpResponseExceptionTestBuilder(
                this.Controller,
                this.ActionName,
                this.CaughtException as HttpResponseException);
        }
    }
}
