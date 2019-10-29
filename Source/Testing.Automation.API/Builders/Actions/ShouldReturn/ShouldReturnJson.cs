namespace Testing.Automation.API.Builders.Actions.ShouldReturn
{
    using System.Web.Http.Results;
    using Contracts.HttpActionResults.Json;
    using HttpActionResults.Json;

    /// <summary>
    /// Trieda obsahuje metody na testovanie vysledku JSON Result.
    /// </summary>
    /// <typeparam name="TActionResult">Vysledok z vyvolanej akcie ASP.NET Web API controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Testuje ci je vysledkom cinnosti, vysledok JSON Result.
        /// </summary>
        /// <returns>JSON test builder.</returns>
        public IJsonTestBuilder Json()
        {
            this.ResultOfType(typeof(JsonResult<>));
            return new JsonTestBuilder<TActionResult>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                this.ActionResult);
        }
    }
}
