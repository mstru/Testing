namespace Testing.Automation.Reporter.Exceptions
{
    using System;

    /// <summary>
    /// Vynimka pre neplatny status code.
    /// </summary>
    public class HttpStatusCodeResultAssertionException : Exception
    {
        /// <summary>
        /// Inicializacia novej instancie HttpStatusCodeResultAssertionException triedy.
        /// </summary>
        /// <param name="message">Sprava pre triedu System.Exception.</param>
        public HttpStatusCodeResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
