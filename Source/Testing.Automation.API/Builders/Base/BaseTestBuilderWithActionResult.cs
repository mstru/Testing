namespace Testing.Automation.API.Builders.Base
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using And;
    using Common;
    using Common.Extensions;
    using Contracts.And;
    using Contracts.Base;
    using global::Testing.Automation.Reporter.Exceptions;
    using Microsoft.CSharp.RuntimeBinder;
    using Utilities;

    /// <summary>
    /// Základná trieda pre všetkých tvorcov testov s výsledkom činnosti.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public abstract class BaseTestBuilderWithActionResult<TActionResult>
        : BaseTestBuilderWithCaughtException, IBaseTestBuilderWithActionResult<TActionResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestBuilderWithActionResult{TActionResult}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="actionResult">Result from the tested action.</param>
        /// <param name="actionAttributes">Collected action attributes from the method call.</param>
        protected BaseTestBuilderWithActionResult(
            ApiController controller,
            string actionName,
            Exception caughtException,
            TActionResult actionResult,
            IEnumerable<object> actionAttributes = null)
            : base(controller, actionName, caughtException, actionAttributes)
        {
            this.ActionResult = actionResult;
        }

        /// <summary>
        /// Získate výsledok akcie, ktorý sa bude testovať.
        /// </summary>
        /// <value>Action result to be tested.</value>
        internal TActionResult ActionResult { get; private set; }

        /// <summary>
        /// Gets the action result which will be tested.
        /// </summary>
        /// <returns>Action result to be tested.</returns>
        public TActionResult AndProvideTheActionResult()
        {
            if (this.ActionResult.GetType() == typeof(VoidActionResult))
            {
                throw new InvalidOperationException("Void methods cannot provide action result because they do not have return value.");
            }

            return this.ActionResult;
        }

        /// <summary>
        /// Získate model odozvy z výsledku akcie.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of response model.</typeparam>
        /// <returns>The response model.</returns>
        protected TResponseModel GetActualModel<TResponseModel>()
        {
            try
            {
                return this.GetActionResultAsDynamic().Content;
            }
            catch (RuntimeBinderException)
            {
                throw new ResponseModelAssertionException(string.Format(
                    "When calling {0} action in {1} expected response model of type {2}, but instead received null.",
                    this.ActionName,
                    this.Controller.GetName(),
                    typeof(TResponseModel).ToFriendlyTypeName()));
            }
        }

        /// <summary>
        /// Inicializuje novú inštanciu builder poskytujúcu metódu AndAlso.
        /// </summary>
        /// <returns>Test builder with AndAlso method.</returns>
        protected IAndTestBuilder<TActionResult> NewAndTestBuilder()
        {
            return new AndTestBuilder<TActionResult>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                this.ActionResult,
                this.ActionLevelAttributes);
        }

        /// <summary>
        /// Vytvorí nový AndProvideTestBuilder.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        protected new IBaseTestBuilderWithActionResult<TActionResult> NewAndProvideTestBuilder()
        {
            return new AndProvideTestBuilder<TActionResult>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                this.ActionResult);
        }

        /// <summary>
        /// Vráti skutočný výsledok akcie odovzdaný ako dynamický typ.
        /// </summary>
        /// <returns>Object of dynamic type.</returns>
        protected dynamic GetActionResultAsDynamic()
        {
            return this.ActionResult.GetType().CastTo<dynamic>(this.ActionResult);
        }
    }
}
