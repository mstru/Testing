namespace Testing.Automation.API.Builders.Contracts.HttpActionResults.Ok
{
    /// <summary>
    /// Used for adding AndAlso() method to the the ok response tests.
    /// </summary>
    public interface IAndOkTestBuilder : IOkTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining ok tests.
        /// </summary>
        /// <returns>The same ok test builder.</returns>
        IOkTestBuilder AndAlso();
    }
}
