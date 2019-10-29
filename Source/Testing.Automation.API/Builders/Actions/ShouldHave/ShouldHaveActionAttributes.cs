namespace Testing.Automation.API.Builders.Actions.ShouldHave
{
    using System;
    using Attributes;
    using Common.Extensions;
    using Contracts.And;
    using Contracts.Attributes;
    using global::Testing.Automation.Reporter.Exceptions;
    using Utilities.Validators;

    /// <summary>
    /// Triede obsahuje metody na testovanie atributov. 
    /// </summary>
    /// <typeparam name="TActionResult">Vysledok z vyvolanej akcie ASP.NET Web API controller.</typeparam>
    public partial class ShouldHaveTestBuilder<TActionResult>
    {
        /// <summary>
        /// Skontroluje, ci testovana akcia nema atribut ziadneho typu.
        /// </summary>
        /// <returns>Test builder with AndAlso method.</returns>
        public IAndTestBuilder<TActionResult> NoActionAttributes()
        {
            AttributesValidator.ValidateNoAttributes(
                this.ActionLevelAttributes,
                this.ThrowNewAttributeAssertionException);

            return this.NewAndTestBuilder();
        }

        /// <summary>
        /// Skontroluje, ci testovana akcia ma aspon 1 atribut akehokolvek typu.
        /// </summary>
        /// <param name="withTotalNumberOf">Volitelna akcia urcuje presny celkovy pocet atributov na testovanje akcii.</param>
        /// <returns>Test builder with AndAlso method.</returns>
        public IAndTestBuilder<TActionResult> ActionAttributes(int? withTotalNumberOf = null)
        {
            AttributesValidator.ValidateNumberOfAttributes(
                this.ActionLevelAttributes,
                this.ThrowNewAttributeAssertionException,
                withTotalNumberOf);

            return this.NewAndTestBuilder();
        }

        /// <summary>
        /// Skontroluje, ci testovana akcia ma specificky atribut.
        /// </summary>
        /// <param name="attributesTestBuilder">Builder na testovanie specifickych atributov vzhladom na akciu.</param>
        /// <returns>Test builder with AndAlso method.</returns>
        public IAndTestBuilder<TActionResult> ActionAttributes(Action<IActionAttributesTestBuilder> attributesTestBuilder)
        {
            var newAttributesTestBuilder = new ActionAttributesTestBuilder(this.Controller, this.ActionName);
            attributesTestBuilder(newAttributesTestBuilder);

            AttributesValidator.ValidateAttributes(
                this.ActionLevelAttributes,
                newAttributesTestBuilder,
                this.ThrowNewAttributeAssertionException);

            return this.NewAndTestBuilder();
        }

        private void ThrowNewAttributeAssertionException(string expectedValue, string actualValue)
        {
            throw new AttributeAssertionException(string.Format(
                "When calling {0} action in {1} expected action to {2}, but {3}.",
                this.ActionName,
                this.Controller.GetName(),
                expectedValue,
                actualValue));
        }
    }
}
