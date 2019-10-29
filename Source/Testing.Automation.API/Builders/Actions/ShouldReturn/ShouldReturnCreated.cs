namespace Testing.Automation.API.Builders.Actions.ShouldReturn
{
    using System.Web.Http.Results;
    using Contracts.HttpActionResults.Created;
    using HttpActionResults.Created;

    /// <summary>
    /// Trieda obsahujúca metódy na testovanie CreatedNegotiatedContentResult {T} alebo CreatedAtRouteNegotiatedContentResult {T}.
    /// </summary>
    /// <typeparam name="TActionResult">Vysledok z vyvolanej akcie ASP.NET Web API controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Testuje na vysledok akcie je CreatedNegotiatedContentResult{T} alebo CreatedAtRouteNegotiatedContentResult{T}.
        /// </summary>
        /// <returns>Created test builder.</returns>
        public ICreatedTestBuilder Created()
        {
            this.ValidateActionReturnType(typeof(CreatedNegotiatedContentResult<>), typeof(CreatedAtRouteNegotiatedContentResult<>));
            return new CreatedTestBuilder<TActionResult>(
                this.Controller,
                    this.ActionName,
                    this.CaughtException,
                    this.ActionResult);
        }
    }
}
