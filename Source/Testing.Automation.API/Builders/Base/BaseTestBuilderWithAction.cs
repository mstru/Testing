﻿namespace Testing.Automation.API.Builders.Base
{
    using System.Collections.Generic;
    using System.Web.Http;
    using Common.Extensions;
    using Contracts.Base;
    using global::Testing.Automation.Reporter.Exceptions;
    using Utilities.Validators;

    /// <summary>
    /// Zakladna trieda pre builders s akciou.
    /// </summary>
    public abstract class BaseTestBuilderWithAction : BaseTestBuilder, IBaseTestBuilderWithAction
    {
        private string actionName;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestBuilderWithAction" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="actionAttributes">Collected action attributes from the method call.</param>
        protected BaseTestBuilderWithAction(
            ApiController controller,
            string actionName,
            IEnumerable<object> actionAttributes = null)
            : base(controller)
        {
            this.ActionName = actionName;
            this.ActionLevelAttributes = actionAttributes;
        }

        /// <summary>
        /// Gets the action name which will be tested.
        /// </summary>
        /// <value>Action name to be tested.</value>
        internal string ActionName
        {
            get
            {
                return this.actionName;
            }

            private set
            {
                CommonValidator.CheckForNotWhiteSpaceString(value, errorMessageName: "ActionName");
                this.actionName = value;
            }
        }

        internal IEnumerable<object> ActionLevelAttributes { get; private set; }

        /// <summary>
        /// Gets the action name which will be tested.
        /// </summary>
        /// <returns>Action name to be tested.</returns>
        public string AndProvideTheActionName()
        {
            return this.ActionName;
        }

        /// <summary>
        /// Gets the action attributes on the called action.
        /// </summary>
        /// <returns>IEnumerable of object representing the attributes or null, if no attributes were collected on the action.</returns>
        public IEnumerable<object> AndProvideTheActionAttributes()
        {
            return this.ActionLevelAttributes;
        }

        /// <summary>
        /// Tests whether the tested action's model state is valid.
        /// </summary>
        protected void CheckValidModelState()
        {
            if (!this.Controller.ModelState.IsValid)
            {
                throw new ModelErrorAssertionException(string.Format(
                    "When calling {0} action in {1} expected to have valid model state with no errors, but it had some.",
                    this.ActionName,
                    this.Controller.GetName()));
            }
        }
    }
}
