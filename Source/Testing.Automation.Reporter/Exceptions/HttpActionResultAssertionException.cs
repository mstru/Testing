namespace Testing.Automation.Reporter.Exceptions
{
    using System;

    /// <summary>
    /// Vynimka pre neplatny typ navratu akcie pri ocakavanom IHttpActionResult.
    /// </summary>
    public class HttpActionResultAssertionException : Exception
    {
        /// <summary>
        /// Inicializacia novej instancie HttpActionResultAssertionException triedy.
        /// </summary>
        /// <param name="message">Sprava pre triedu System.Exception.</param>
        public HttpActionResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
