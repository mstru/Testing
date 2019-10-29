namespace Testing.Automation.API.Builders.Actions
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using Base;
    using Common.Extensions;
    using Contracts.Actions;
    using ShouldHave;
    using ShouldReturn;
    using global::Testing.Automation.Reporter.Exceptions;
    using Utilities.Validators;

    /// <summary>
    /// Pouziva sa na vytovorenie cinnosti, ktora sa bude testovat.
    /// </summary>
    /// <typeparam name="TActionResult">Vysledok z vyvolanej akcie ASP.NET Web API controller.</typeparam>
    public class ActionResultTestBuilder<TActionResult>
        : BaseTestBuilderWithActionResult<TActionResult>, IActionResultTestBuilder<TActionResult>
    {
        /// <summary>
        /// Inicializacia novej instancie <see cref="ActionResultTestBuilder{TActionResult}" /> triedy.
        /// </summary>
        /// <param name="controller">Controller na ktorom bude cinnost testovana.</param>
        /// <param name="actionName">Nazov testovanej akcie.</param>
        /// <param name="caughtException">Chyba vynimky.</param>
        /// <param name="actionResult">Vysledok testovanej akcie.</param>
        /// <param name="actionAttributes">Zoznam atributov volanej akcie.</param>
        public ActionResultTestBuilder(
            ApiController controller,
            string actionName,
            Exception caughtException,
            TActionResult actionResult,
            IEnumerable<object> actionAttributes)
            : base(controller, actionName, caughtException, actionResult, actionAttributes)
        {
        }

        /// <summary>
        /// Používa sa na testovanie atribútov akcie a stavu modelu.
        /// </summary>
        /// <returns>Should have test builder.</returns>
        public IShouldHaveTestBuilder<TActionResult> ShouldHave()
        {
            return new ShouldHaveTestBuilder<TActionResult>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                this.ActionResult,
                this.ActionLevelAttributes);
        }

        /// <summary>
        /// Používa sa na testovanie toho, či akcia udeľuje výnimku.
        /// </summary>
        /// <returns>Should throw test builder.</returns>
        public IShouldThrowTestBuilder ShouldThrow()
        {
            if (this.CaughtException == null)
            {
                throw new InvalidCallAssertionException(string.Format(
                    "When calling {0} action in {1} thrown exception was expected, but in fact none was caught.",
                    this.ActionName,
                    this.Controller.GetName()));
            }

            return new ShouldThrowTestBuilder(this.Controller, this.ActionName, this.CaughtException);
        }

        /// <summary>
        /// Používa sa na testovanie výsledkov vrátených akcií.
        /// </summary>
        /// <returns>Should return test builder.</returns>
        public IShouldReturnTestBuilder<TActionResult> ShouldReturn()
        {
            CommonValidator.CheckForException(this.CaughtException);
            return new ShouldReturnTestBuilder<TActionResult>(this.Controller, this.ActionName, this.CaughtException, this.ActionResult);
        }
    }
}
