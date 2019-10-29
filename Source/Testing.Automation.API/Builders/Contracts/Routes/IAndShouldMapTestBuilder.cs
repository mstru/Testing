namespace Testing.Automation.API.Builders.Contracts.Routes
{
    /// <summary>
    /// Used for adding And() method to the route request builder.
    /// </summary>
    public interface IAndShouldMapTestBuilder : IShouldMapTestBuilder
    {
        /// <summary>
        /// And method for better readability when building route HTTP request message.
        /// </summary>
        /// <returns>The same controller builder.</returns>
        IShouldMapTestBuilder And();
    }
}
