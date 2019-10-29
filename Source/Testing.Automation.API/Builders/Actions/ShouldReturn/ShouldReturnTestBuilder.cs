namespace Testing.Automation.API.Builders.Actions.ShouldReturn
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using Base;
    using Common.Extensions;
    using Contracts.Actions;
    using global::Testing.Automation.Reporter.Exceptions;
    using Utilities;
    using Utilities.Validators;

    /// <summary>
    /// Trieda sa používa sa na testovanie výsledkov vrátených akcií.
    /// </summary>
    /// <typeparam name="TActionResult">Vysledok z vyvolanej akcie ASP.NET Web API controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
        : BaseTestBuilderWithActionResult<TActionResult>, IShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Inicializacia novej instancie <see cref="ShouldReturnTestBuilder{TActionResult}" /> triedy.
        /// </summary>
        /// <param name="controller">Controller na ktorom bude cinnost testovana.</param>
        /// <param name="actionName">Nazov testovanej akcie.</param>
        /// <param name="caughtException">Chyba vynimky.</param>
        /// <param name="actionResult">Vysledok testovanej akcie.</param>
        public ShouldReturnTestBuilder(
            ApiController controller,
            string actionName,
            Exception caughtException,
            TActionResult actionResult)
            : base(controller, actionName, caughtException, actionResult)
        {
        }

        private void ValidateActionReturnType(Type typeOfExpectedReturnValue, bool canBeAssignable = false, bool allowDifferentGenericTypeDefinitions = false)
        {
            CommonValidator.CheckForException(this.CaughtException);

            var typeOfActionResult = ActionResult.GetType();

            var isAssignableCheck = canBeAssignable && Reflection.AreNotAssignable(typeOfExpectedReturnValue, typeOfActionResult);
            if (isAssignableCheck && allowDifferentGenericTypeDefinitions
                && Reflection.IsGeneric(typeOfExpectedReturnValue) && Reflection.IsGenericTypeDefinition(typeOfExpectedReturnValue))
            {
                isAssignableCheck = Reflection.AreAssignableByGeneric(typeOfExpectedReturnValue, typeOfActionResult);

                if (!isAssignableCheck && !typeOfActionResult.IsGenericType)
                {
                    isAssignableCheck = true;
                }
                else
                {
                    isAssignableCheck = 
                        !Reflection.ContainsGenericTypeDefinitionInterface(typeOfExpectedReturnValue, typeOfActionResult);
                }
            }

            var strictlyEqualCheck = !canBeAssignable && Reflection.AreDifferentTypes(typeOfExpectedReturnValue, typeOfActionResult);

            var invalid = isAssignableCheck || strictlyEqualCheck;
            if (strictlyEqualCheck)
            {
                var genericTypeDefinitionCheck = Reflection.AreAssignableByGeneric(typeOfExpectedReturnValue, typeOfActionResult);

                if (genericTypeDefinitionCheck)
                {
                    invalid = false;
                }
            }

            if (invalid && typeOfExpectedReturnValue.IsGenericTypeDefinition && typeOfActionResult.IsGenericType)
            {
                var actionResultGenericDefinition = typeOfActionResult.GetGenericTypeDefinition();
                if (actionResultGenericDefinition == typeOfExpectedReturnValue)
                {
                    invalid = false;
                }
            }

            if (invalid && typeOfExpectedReturnValue.IsGenericType && typeOfActionResult.IsGenericType)
            {
                invalid = !Reflection.AreAssignableByGeneric(typeOfExpectedReturnValue, typeOfActionResult);
            }

            if (invalid)
            {
                this.ThrowNewGenericHttpActionResultAssertionException(
                    typeOfExpectedReturnValue.ToFriendlyTypeName(),
                    typeOfActionResult.ToFriendlyTypeName());
            }
        }

        private void ValidateActionReturnType<TExpectedType>(bool canBeAssignable = false, bool allowDifferentGenericTypeDefinitions = false)
        {
            var typeOfResponseData = typeof(TExpectedType);
            this.ValidateActionReturnType(typeOfResponseData, canBeAssignable, allowDifferentGenericTypeDefinitions);
        }

        private void ValidateActionReturnType(params Type[] returnTypes)
        {
            var typeOfActionResult = this.ActionResult.GetType();
            if (returnTypes.All(t => !Reflection.AreAssignableByGeneric(t, typeOfActionResult)))
            {
                this.ThrowNewGenericHttpActionResultAssertionException(
                    string.Join(" or ", returnTypes.Select(t => t.ToFriendlyTypeName())),
                    typeOfActionResult.ToFriendlyTypeName());
            }
        }

        private void ThrowNewGenericHttpActionResultAssertionException(
            string typeNameOfExpectedReturnValue,
            string typeNameOfActionResult)
        {
            throw new HttpActionResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected action result to be {2}, but instead received {3}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    typeNameOfExpectedReturnValue,
                    typeNameOfActionResult));
        }
    }
}
