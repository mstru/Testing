namespace Testing.Automation.API.Builders.Contracts.Servers
{
    using HttpResponseMessages;

    /// <summary>
    /// Poskytuje možnosti otestovania odpovede HTTP zo servera.
    /// </summary>
    public interface IServerTestBuilder
    {
        /// <summary>
        /// Testuje konkrétnu odpoveď HTTP.
        /// </summary>
        /// <returns>HTTP response message test builder.</returns>
        IHttpHandlerResponseMessageWithTimeTestBuilder ShouldReturnHttpResponseMessage();
    }
}
