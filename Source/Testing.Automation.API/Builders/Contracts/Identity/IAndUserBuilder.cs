namespace Testing.Automation.API.Builders.Contracts.Identity
{
    /// <summary>
    /// Used for adding AndAlso() method to the the built user.
    /// </summary>
    public interface IAndUserBuilder : IUserBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when building user.
        /// </summary>
        /// <returns>The same user builder.</returns>
        IUserBuilder AndAlso();
    }
}
