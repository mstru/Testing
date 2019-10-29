namespace Testing.Automation.API.Builders.Actions.ShouldHave
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using Base;
    using Contracts.Actions;

    /// <summary>
    /// Trieda obsahuje metody na testovanie atributov a stavu modelu.
    /// </summary>
    /// <typeparam name="TActionResult">Vysledok z vyvolanej akcie ASP.NET Web API controller.</typeparam>
    public partial class ShouldHaveTestBuilder<TActionResult>
        : BaseTestBuilderWithActionResult<TActionResult>, IShouldHaveTestBuilder<TActionResult>
    {
        /// <summary>
        /// Inicializacia novej instancie <see cref="ShouldHaveTestBuilder{TActionResult}" /> triedy.
        /// </summary>
        /// <param name="controller">Controller, ktory bude testovany.</param>
        /// <param name="actionName">Nazov testovanej akcie.</param>
        /// <param name="caughtException">Chytena vynimka vyvolanej akcie.</param>
        /// <param name="actionResult">Vysledok na vykonanu akciu.</param>
        /// <param name="actionAttributes">Kolecia atribotov vykonavanej akcie.</param>
        public ShouldHaveTestBuilder(
            ApiController controller,
            string actionName,
            Exception caughtException,
            TActionResult actionResult,
            IEnumerable<object> actionAttributes)
            : base(controller, actionName, caughtException, actionResult, actionAttributes)
        {
        }
    }
}
