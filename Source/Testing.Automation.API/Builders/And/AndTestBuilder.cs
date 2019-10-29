namespace Testing.Automation.API.Builders.And
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using Actions;
    using Base;
    using Contracts.Actions;
    using Contracts.And;

    /// <summary>
    /// Trieda obashuje metodu AndAlso() metoda umoznuje dodatocne tvrdenia po testoch stavu modelu.
    /// </summary>
    /// <typeparam name="TActionResult">Vysledok z vyvolanej akcie ASP.NET Web API controller.</typeparam>
    public class AndTestBuilder<TActionResult> : BaseTestBuilderWithActionResult<TActionResult>,
        IAndTestBuilder<TActionResult>
    {
        /// <summary>
        /// Inicializacia novej instancie <see cref="AndTestBuilder{TActionResult}" /> triedy.
        /// </summary>
        /// <param name="controller">Controller na ktorom bude cinnost testovana.</param>
        /// <param name="actionName">Nazov testovanej akcie.</param>
        /// <param name="caughtException">Chyba vynimky.</param>
        /// <param name="actionResult">Vysledok akcie.</param>
        /// <param name="actionAttributes">Zoznam atributov volanej metody.</param>
        public AndTestBuilder(
            ApiController controller,
            string actionName,
            Exception caughtException,
            TActionResult actionResult,
            IEnumerable<object> actionAttributes = null)
            : base(controller, actionName, caughtException, actionResult, actionAttributes)
        {
        }

        /// <summary>
        /// Metóda umožňujúca dodatočné tvrdenia po testoch stavu modelu.
        /// </summary>
        /// <returns>Builder for testing the action result.</returns>
        public IActionResultTestBuilder<TActionResult> AndAlso()
        {
            return new ActionResultTestBuilder<TActionResult>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                this.ActionResult,
                this.ActionLevelAttributes);
        }
    }
}
