namespace Testing.Automation.API.Builders.Actions.ShouldReturn
{
    using System.Web.Http.Results;
    using Contracts.HttpActionResults.Ok;
    using HttpActionResults.Ok;

    /// <summary>
    /// Trieda obsahujúca metódy testovania OkResult a OkNegotiatedContentResult {T}.
    /// </summary>
    /// <typeparam name="TActionResult">Vysledok z vyvolanej akcie ASP.NET Web API controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Testuje, či je výsledok akcie OkResult.
        /// </summary>
        /// <returns>Ok test builder.</returns>
        public IOkTestBuilder Ok()
        {
            var actionResultAsOkResult = this.ActionResult as OkResult;
            if (actionResultAsOkResult != null)
            {
                this.ResultOfType<OkResult>();
            }
            else
            {
                this.ValidateActionReturnType(typeof(OkNegotiatedContentResult<>), allowDifferentGenericTypeDefinitions: true);
            }

            return new OkTestBuilder<TActionResult>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                this.ActionResult);
        }
    }
}
