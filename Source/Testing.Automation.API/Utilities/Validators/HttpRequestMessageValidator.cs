namespace Testing.Automation.API.Utilities.Validators
{
    using System.Net.Http;
    using global::Testing.Automation.Reporter.Exceptions;

    /// <summary>
    /// Trieda validátora obsahujúca validácie správy žiadosti HTTP.
    /// </summary>
    public static class HttpRequestMessageValidator
    {
        /// <summary>
        /// Skontroluje, či môžu byť hlavičky pridané do HttpRequestMessage.
        /// </summary>
        /// <param name="requestMessage">HttpRequestMessage to validate.</param>
        public static void ValidateContent(HttpRequestMessage requestMessage)
        {
            if (requestMessage.Content == null)
            {
                throw new InvalidHttpRequestMessageException("When building HttpRequestMessage expected content to be initialized and set in order to add content headers.");
            }
        }
    }
}
