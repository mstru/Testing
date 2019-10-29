﻿namespace Testing.Automation.API.Builders.Contracts.Models
{
    using Base;

    /// <summary>
    /// Base interface for all response model test builders.
    /// </summary>
    public interface IBaseResponseModelTestBuilder : IBaseTestBuilderWithCaughtException
    {
        /// <summary>
        /// Tests whether response model is returned from the invoked action.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <returns>Builder for testing the response model errors.</returns>
        IModelDetailsTestBuilder<TResponseModel> WithResponseModelOfType<TResponseModel>();

        /// <summary>
        /// Tests whether a deeply equal object to the provided one is returned from the invoked action.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <param name="expectedModel">Expected model to be returned.</param>
        /// <returns>Builder for testing the response model errors.</returns>
        IModelDetailsTestBuilder<TResponseModel> WithResponseModel<TResponseModel>(TResponseModel expectedModel);
    }
}
