namespace Testing.Automation.API.Builders.Actions.ShouldReturn
{
    using System.Net;
    using System.Web.Http.Results;
    using Common.Extensions;
    using Contracts.Base;
    using global::Testing.Automation.Reporter.Exceptions;

    /// <summary>
    /// Trieda obsahujúca metódy na testovanie StatusCodeResult.
    /// </summary>
    /// <typeparam name="TActionResult">Vysledok z vyvolanej akcie ASP.NET Web API controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Testuje, či výsledok činnosti je StatusCodeResult.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        public IBaseTestBuilderWithActionResult<TActionResult> StatusCode()
        {
            this.ResultOfType<StatusCodeResult>();
            return this.NewAndProvideTestBuilder();
        }

        /// <summary>
        /// Testuje, či výsledok akcie je StatusCodeResult a je rovnaký ako v prípade HttpStatusCode..
        /// </summary>
        /// <param name="statusCode">HttpStatusCode.</param>
        /// <returns>Base test builder with action result.</returns>
        public IBaseTestBuilderWithActionResult<TActionResult> StatusCode(HttpStatusCode statusCode)
        {
            var statusCodeResult = this.GetReturnObject<StatusCodeResult>();
            var actualStatusCode = statusCodeResult.StatusCode;
            if (statusCodeResult.StatusCode != statusCode)
            {
                throw new HttpStatusCodeResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected to have {2} ({3}) status code, but received {4} ({5}).",
                    this.ActionName,
                    this.Controller.GetName(),
                    (int)statusCode,
                    statusCode,
                    (int)actualStatusCode,
                    actualStatusCode));
            }

            return this.NewAndProvideTestBuilder();
        }
    }
}
