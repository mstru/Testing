namespace Testing.Automation.API.Builders.Actions
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using Base;
    using Common;
    using Contracts.Actions;
    using Contracts.Base;
    using ShouldHave;
    using Utilities.Validators;

    /// <summary>
    /// Používa sa na testovanie neplatných akcií.
    /// </summary>
    public class VoidActionResultTestBuilder : BaseTestBuilderWithCaughtException, IVoidActionResultTestBuilder
    {
        /// <summary>
        /// Inicializacia novej instancie <see cref="VoidActionResultTestBuilder" /> triedy.
        /// </summary>
        /// <param name="controller">Controller na ktorom bude cinnost testovana.</param>
        /// <param name="actionName">Nazov testovanej akcie.</param>
        /// <param name="caughtException">Chyba vynimky.</param>
        /// <param name="actionAttributes">Zoznam atributov volanej akcie.</param>
        public VoidActionResultTestBuilder(
            ApiController controller,
            string actionName,
            Exception caughtException,
            IEnumerable<object> actionAttributes)
            : base(controller, actionName, caughtException, actionAttributes)
        {
        }

        /// <summary>
        /// Testuje, či je výsledok činnosti neplatný.
        /// </summary>
        /// <returns>Base test builder.</returns>
        public IBaseTestBuilderWithCaughtException ShouldReturnEmpty()
        {
            CommonValidator.CheckForException(this.CaughtException);
            return this.NewAndProvideTestBuilder();
        }

        /// <summary>
        /// Používa sa na testovanie atribútov akcie a stavu modelu.
        /// </summary>
        /// <returns>Should have test builder.</returns>
        public IShouldHaveTestBuilder<VoidActionResult> ShouldHave()
        {
            return new ShouldHaveTestBuilder<VoidActionResult>(
                this.Controller, 
                this.ActionName, 
                this.CaughtException, 
                VoidActionResult.Create(),
                this.ActionLevelAttributes);
        }

        /// <summary>
        /// Používa sa na testovanie toho, či akcia udeľuje výnimku.
        /// </summary>
        /// <returns>Should throw test builder.</returns>
        public IShouldThrowTestBuilder ShouldThrow()
        {
            return new ShouldThrowTestBuilder(this.Controller, this.ActionName, this.CaughtException);
        }
    }
}
