namespace Testing.Automation.API.Builders.Actions.ShouldHave
{
    using System.Linq;
    using Common.Extensions;
    using Contracts.And;
    using Contracts.Models;
    using global::Testing.Automation.Reporter.Exceptions;
    using Models;

    /// <summary>
    /// Trieda obsahuje metody na testovanie stavu modelu.
    /// </summary>
    /// <typeparam name="TActionResult">Vysledok z vyvolanej akcie ASP.NET Web API controller.</typeparam>
    public partial class ShouldHaveTestBuilder<TActionResult>
    {
        /// <summary>
        /// Poskytuje spôsob pokračovania testovacieho prípadu so špecifickými stavovými chybami modelu.
        /// </summary>
        /// <typeparam name="TRequestModel">Typ modelu, ktory sa ma testovat na chyby.</typeparam>
        /// <returns>Response model test builder.</returns>
        public IModelErrorTestBuilder<TRequestModel> ModelStateFor<TRequestModel>()
        {
            return new ModelErrorTestBuilder<TRequestModel>(this.Controller, this.ActionName, this.CaughtException);
        }

        /// <summary>
        /// Skontroluje ci model poskytnuty testovacej akcii je platny.
        /// </summary>
        /// <returns>Test builder with AndAlso method.</returns>
        public IAndTestBuilder<TActionResult> ValidModelState()
        {
            this.CheckValidModelState();
            return this.NewAndTestBuilder();
        }

        /// <summary>
        /// Skontroluje ci model poskytnuty testovacej akcii nie je platny.
        /// </summary>
        /// <param name="withNumberOfErrors">Ocakavany pocet chyb. Predvolena hodnota je null.</param>
        /// <returns>Test builder with AndAlso method.</returns>
        public IAndTestBuilder<TActionResult> InvalidModelState(int? withNumberOfErrors = null)
        {
            var actualModelStateErrors = this.Controller.ModelState.Values.SelectMany(c => c.Errors).Count();
            if (actualModelStateErrors == 0
                || (withNumberOfErrors != null && actualModelStateErrors != withNumberOfErrors))
            {
                throw new ModelErrorAssertionException(string.Format(
                    "When calling {0} action in {1} expected to have invalid model state{2}, {3}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    withNumberOfErrors == null ? string.Empty : string.Format(" with {0} errors", withNumberOfErrors),
                    withNumberOfErrors == null ? "but was in fact valid" : string.Format("but in fact contained {0}", actualModelStateErrors)));
            }

            return this.NewAndTestBuilder();
        }
    }
}
