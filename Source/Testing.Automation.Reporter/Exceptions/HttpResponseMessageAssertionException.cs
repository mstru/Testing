namespace Testing.Automation.Reporter.Exceptions
{
    using System;

    /// <summary>
    /// Výnimka pre neplatný výsledok správy HTTP.
    /// </summary>
    public class HttpResponseMessageAssertionException : Exception
    {
        /// <summary>
        /// Inicializacia novej instancie HttpResponseMessageAssertionException triedy.
        /// </summary>
        /// <param name="message">Sprava pre triedu System.Exception.</param>
        public HttpResponseMessageAssertionException(string message)
            : base(message)
        {
        }
    }
}
