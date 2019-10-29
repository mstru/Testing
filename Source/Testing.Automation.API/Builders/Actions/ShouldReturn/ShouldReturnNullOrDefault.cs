namespace Testing.Automation.API.Builders.Actions.ShouldReturn
{
    using Common.Extensions;
    using Contracts.Base;
    using global::Testing.Automation.Reporter.Exceptions;
    using Utilities;
    using Utilities.Validators;

    /// <summary>
    /// Trieda obsahujúca metódy na testovanie výsledkov null alebo predvolených hodnôt.
    /// </summary>
    /// <typeparam name="TActionResult">Vysledok z vyvolanej akcie ASP.NET Web API controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Testuje, či je výsledok akcie ma platnu predvolenu hodnotu.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        public IBaseTestBuilderWithActionResult<TActionResult> DefaultValue()
        {
            if (!this.CheckValidDefaultValue())
            {
                this.ThrowNewHttpActionResultAssertionException(string.Format(
                    "the default value of {0}, but in fact it was not.",
                    typeof(TActionResult).ToFriendlyTypeName()));
            }

            return this.NewAndProvideTestBuilder();
        }

        /// <summary>
        /// Testuje ci vysledok akcie je null.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        public IBaseTestBuilderWithActionResult<TActionResult> Null()
        {
            CommonValidator.CheckIfTypeCanBeNull(typeof(TActionResult));
            if (!this.CheckValidDefaultValue())
            {
                this.ThrowNewHttpActionResultAssertionException(string.Format(
                    "null, but instead received {0}.",
                    typeof(TActionResult).ToFriendlyTypeName()));
            }

            return this.NewAndProvideTestBuilder();
        }

        /// <summary>
        /// Testuje ci vysledok akcie nie je null.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        public IBaseTestBuilderWithActionResult<TActionResult> NotNull()
        {
            CommonValidator.CheckIfTypeCanBeNull(typeof(TActionResult));
            if (this.CheckValidDefaultValue())
            {
                this.ThrowNewHttpActionResultAssertionException(string.Format(
                    "not null, but it was {0} object.",
                    typeof(TActionResult).ToFriendlyTypeName()));
            }

            return this.NewAndProvideTestBuilder();
        }

        private bool CheckValidDefaultValue()
        {
            return CommonValidator.CheckForDefaultValue(this.ActionResult) && this.CaughtException == null;
        }

        private void ThrowNewHttpActionResultAssertionException(string message)
        {
            throw new HttpActionResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected action result to be {2}",
                    this.ActionName,
                    this.Controller.GetName(),
                    message));
        }
    }
}
