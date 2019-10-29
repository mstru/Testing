namespace Testing.Automation.API.Builders.Actions.ShouldReturn
{
    using System.Web.Http.Results;
    using Contracts.HttpActionResults.BadRequest;
    using HttpActionResults.BadRequest;

    /// <summary>
    /// Trieda obsahujúca metódy na testovanie BadRequestResult, InvalidModelStateResult alebo BadRequestErrorMessageResult.
    /// </summary>
    /// <typeparam name="TActionResult">Vysledok z vyvolanej akcie ASP.NET Web API controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Testuje ci vysledok akcie je BadRequestResult, InvalidModelStateResult or BadRequestErrorMessageResult.
        /// </summary>
        /// <returns>Bad request test builder.</returns>
        public IBadRequestTestBuilder BadRequest()
        {
            if (this.ActionResult as BadRequestErrorMessageResult != null)
            {
                return this.ReturnBadRequestTestBuilder<BadRequestErrorMessageResult>();
            }

            if (this.ActionResult as InvalidModelStateResult != null)
            {
                return this.ReturnBadRequestTestBuilder<InvalidModelStateResult>();
            }

            return this.ReturnBadRequestTestBuilder<BadRequestResult>();
        }

        private IBadRequestTestBuilder ReturnBadRequestTestBuilder<TBadRequestResult>()
            where TBadRequestResult : class
        {
            var badRequestResult = this.GetReturnObject<TBadRequestResult>();
            return new BadRequestTestBuilder<TBadRequestResult>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                badRequestResult);
        }
    }
}
