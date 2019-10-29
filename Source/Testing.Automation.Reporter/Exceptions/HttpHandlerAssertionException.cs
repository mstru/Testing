namespace Testing.Automation.Reporter.Exceptions
{
    using System;

    /// <summary>
    /// Vynimka pre invalid HTTP handler.
    /// </summary>
    public class HttpHandlerAssertionException : Exception
    {
        /// <summary>
        /// Inicializacia novej instancie HttpHandlerAssertionException triedy.
        /// </summary>
        /// <param name="message">Sprava pre triedu System.Exception.</param>
        public HttpHandlerAssertionException(string message)
            : base(message)
        {
        }
    }
}
