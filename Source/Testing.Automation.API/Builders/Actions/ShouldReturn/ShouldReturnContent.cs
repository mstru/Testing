namespace Testing.Automation.API.Builders.Actions.ShouldReturn
{
    using System.Web.Http.Results;
    using Contracts.HttpActionResults.Content;
    using HttpActionResults.Content;

    /// <summary>
    /// Trieda obsahujúca metódy testovania NegotiatedContentResult {T} alebo FormattedContentResult {T}.
    /// </summary>
    /// <typeparam name="TActionResult">Vysledok z vyvolanej akcie ASP.NET Web API controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Testuje na vysledok akcie je NegotiatedContentResult{T} alebo FormattedContentResult{T}.
        /// </summary>
        /// <returns>Content test builder.</returns>
        public IContentTestBuilder Content()
        {
            this.ValidateActionReturnType(typeof(NegotiatedContentResult<>), typeof(FormattedContentResult<>));
            return new ContentTestBuilder<TActionResult>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                this.ActionResult);
        }
    }
}
