﻿namespace Testing.Automation.API.Builders.Contracts.Base
{
    /// <summary>
    /// Base interface for all test builders with action result.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public interface IBaseTestBuilderWithActionResult<out TActionResult> : IBaseTestBuilderWithCaughtException
    {
        /// <summary>
        /// Gets the tested action result.
        /// </summary>
        /// <returns>Tested action result.</returns>
        TActionResult AndProvideTheActionResult();
    }
}
