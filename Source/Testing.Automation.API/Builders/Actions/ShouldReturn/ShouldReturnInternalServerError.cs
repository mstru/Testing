namespace Testing.Automation.API.Builders.Actions.ShouldReturn
{
    using System.Web.Http.Results;
    using Contracts.HttpActionResults.InternalServerError;
    using HttpActionResults.InternalServerError;

    /// <summary>
    /// Trieda obsahujúca metódy na testovanie InternalServerErrorResult alebo ExceptionResult.
    /// </summary>
    /// <typeparam name="TActionResult">Vysledok z vyvolanej akcie ASP.NET Web API controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Testuje ci vysledok cinnosti je InternalServerErrorResult or ExceptionResult.
        /// </summary>
        /// <returns>Internal server error test builder.</returns>
        public IInternalServerErrorTestBuilder InternalServerError()
        {
            if (this.ActionResult as ExceptionResult != null)
            {
                return this.ReturnInternalServerErrorTestBuilder<ExceptionResult>();
            }

            return this.ReturnInternalServerErrorTestBuilder<InternalServerErrorResult>();
        }

        private IInternalServerErrorTestBuilder ReturnInternalServerErrorTestBuilder<TInternalServerErrorResult>()
            where TInternalServerErrorResult : class
        {
            var internalServerErrorResult = this.GetReturnObject<TInternalServerErrorResult>();
            return new InternalServerErrorTestBuilder<TInternalServerErrorResult>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                internalServerErrorResult);
        }
    }
}
