namespace Testing.Automation.Reporter.Exceptions
{
    using System;

    /// <summary>
    /// Vynimka pre invalid HTTP request message.
    /// </summary>
    public class InvalidHttpRequestMessageException : Exception
    {
        /// <summary>
        /// Inicializacia novej instancie InvalidHttpRequestMessageException triedy.
        /// </summary>
        /// <param name="message">Sprava pre triedu System.Exception.</param>
        public InvalidHttpRequestMessageException(string message)
            : base(message)
        {
        }
    }
}
