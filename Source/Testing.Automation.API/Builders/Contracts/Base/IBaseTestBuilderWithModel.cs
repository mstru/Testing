﻿namespace Testing.Automation.API.Builders.Contracts.Base
{
    /// <summary>
    /// Base interface for all test builders with model.
    /// </summary>
    /// <typeparam name="TModel">Model returned from action result.</typeparam>
    public interface IBaseTestBuilderWithModel<out TModel> : IBaseTestBuilderWithCaughtException
    {
        /// <summary>
        /// Gets the model returned from an action result.
        /// </summary>
        /// <returns>Model returned from action result.</returns>
        TModel AndProvideTheModel();
    }
}
