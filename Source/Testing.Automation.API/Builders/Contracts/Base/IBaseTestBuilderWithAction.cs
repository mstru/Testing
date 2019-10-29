﻿namespace Testing.Automation.API.Builders.Contracts.Base
{
    using System.Collections.Generic;

    /// <summary>
    /// Base class for all test builders with action call.
    /// </summary>
    public interface IBaseTestBuilderWithAction : IBaseTestBuilder
    {
        /// <summary>
        /// Gets the action name which will be tested.
        /// </summary>
        /// <returns>Action name to be tested.</returns>
        string AndProvideTheActionName();

        /// <summary>
        /// Gets the action attributes on the called action.
        /// </summary>
        /// <returns>IEnumerable of object representing the attributes or null, if no attributes were collected on the action.</returns>
        IEnumerable<object> AndProvideTheActionAttributes();
    }
}
