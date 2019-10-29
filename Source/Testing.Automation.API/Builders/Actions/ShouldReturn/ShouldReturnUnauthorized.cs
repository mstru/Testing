namespace Testing.Automation.API.Builders.Actions.ShouldReturn
{
    using System.Web.Http.Results;
    using Contracts.HttpActionResults.Unauthorized;
    using HttpActionResults.Unauthorized;

    /// <summary>
    /// Trieda obsahujúca metódy na testovanie  UnauthorizedResult.
    /// </summary>
    /// <typeparam name="TActionResult">Vysledok z vyvolanej akcie ASP.NET Web API controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Testuje, či je výsledok akcie UnauthorizedResult.
        /// </summary>
        /// <returns>Unauthorized result test builder.</returns>
        public IUnauthorizedTestBuilder Unauthorized()
        {
            var unathorizedResult = this.GetReturnObject<UnauthorizedResult>();
            return new UnauthorizedTestBuilder(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                unathorizedResult);
        }
    }
}
