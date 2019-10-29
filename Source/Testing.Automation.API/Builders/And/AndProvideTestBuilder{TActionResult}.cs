namespace Testing.Automation.API.Builders.And
{
    using System;
    using System.Web.Http;
    using Base;

    /// <summary>
    /// Poskytuje informácie o kontrole, akcii a výsledkoch akcie.
    /// </summary>
    /// <typeparam name="TActionResult">Vysledok z vyvolanej akcie ASP.NET Web API controller.</typeparam>
    public class AndProvideTestBuilder<TActionResult> : BaseTestBuilderWithActionResult<TActionResult>
    {
        /// <summary>
        /// Inicializacia novej instancie <see cref="AndProvideTestBuilder{TActionResult}" /> triedy.
        /// </summary>
        /// <param name="controller">Controller na ktorom bude cinnost testovana.</param>
        /// <param name="actionName">Nazov testovanej akcie.</param>
        /// <param name="caughtException">Chyba vynimky.</param>
        /// <param name="actionResult">Vysledok vykonanej akcie.</param>
        public AndProvideTestBuilder(
            ApiController controller,
            string actionName,
            Exception caughtException,
            TActionResult actionResult)
            : base(controller, actionName, caughtException, actionResult)
        {
        }
    }
}
