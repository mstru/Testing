﻿namespace Testing.Automation.API.Builders.Base
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using And;
    using Contracts.Base;

    /// <summary>
    /// Base class for test builders with caught exception.
    /// </summary>
    public abstract class BaseTestBuilderWithCaughtException
        : BaseTestBuilderWithAction, IBaseTestBuilderWithCaughtException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestBuilderWithCaughtException" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="actionAttributes">Collected action attributes from the method call.</param>
        protected BaseTestBuilderWithCaughtException(
            ApiController controller,
            string actionName,
            Exception caughtException,
            IEnumerable<object> actionAttributes = null)
            : base(controller, actionName, actionAttributes)
        {
            this.CaughtException = caughtException;
        }

        internal Exception CaughtException { get; private set; }

        /// <summary>
        /// Gets the thrown exception in the tested action.
        /// </summary>
        /// <returns>The exception instance or null, if no exception was caught.</returns>
        public Exception AndProvideTheCaughtException()
        {
            return this.CaughtException;
        }

        /// <summary>
        /// Creates new AndProvideTestBuilder.
        /// </summary>
        /// <returns>Base test builder.</returns>
        protected IBaseTestBuilderWithCaughtException NewAndProvideTestBuilder()
        {
            return new AndProvideTestBuilder(this.Controller, this.ActionName, this.CaughtException);
        }
    }
}
