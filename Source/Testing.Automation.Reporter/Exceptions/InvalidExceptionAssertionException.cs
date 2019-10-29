namespace Testing.Automation.Reporter.Exceptions
{
    using System;

    /// <summary>
    /// Vynimka pre invalid expected exceptions.
    /// </summary>
    public class InvalidExceptionAssertionException : Exception
    {
        /// <summary>
        /// Inicializacia novej instancie InvalidExceptionAssertionException triedy.
        /// </summary>
        /// <param name="message">Sprava pre triedu System.Exception.</param>
        public InvalidExceptionAssertionException(string message)
            : base(message)
        {
        }
    }
}
