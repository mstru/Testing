namespace Testing.Automation.API.Builders.Actions.ShouldReturn
{
    using System.Web.Http.Results;
    using Contracts.HttpActionResults.Redirect;
    using HttpActionResults.Redirect;

    /// <summary>
    /// Trieda obsahujúca metódy na testovanie RedirectResult alebo RedirectToRouteResult.
    /// </summary>
    /// <typeparam name="TActionResult">Vysledok z vyvolanej akcie ASP.NET Web API controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Testuje ci je vysledok akcie RedirectResult alebo RedirectToRouteResult.
        /// </summary>
        /// <returns>Redirect test builder.</returns>
        public IRedirectTestBuilder Redirect()
        {
            var actionResultAsRedirectResult = this.ActionResult as RedirectToRouteResult;
            if (actionResultAsRedirectResult != null)
            {
                return this.ReturnRedirectTestBuilder<RedirectToRouteResult>();
            }

            return this.ReturnRedirectTestBuilder<RedirectResult>();
        }

        private IRedirectTestBuilder ReturnRedirectTestBuilder<TRedirectResult>()
            where TRedirectResult : class
        {
            var redirectResult = this.GetReturnObject<TRedirectResult>();
            return new RedirectTestBuilder<TRedirectResult>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                redirectResult);
        }
    }
}
