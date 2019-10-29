﻿namespace Testing.Automation.API.Builders.Contracts.HttpActionResults.Json
{
    /// <summary>
    /// Used for adding AndAlso() method to the the JSON response tests.
    /// </summary>
    public interface IAndJsonTestBuilder : IJsonTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining JSON result tests.
        /// </summary>
        /// <returns>JSON result test builder.</returns>
        IJsonTestBuilder AndAlso();
    }
}
