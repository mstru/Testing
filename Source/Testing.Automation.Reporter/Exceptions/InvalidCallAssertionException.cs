namespace Testing.Automation.Reporter.Exceptions
{
    using System;

    /// <summary>
    /// Vynimka pre invalid test call.
    /// </summary>
    public class InvalidCallAssertionException : Exception
    {
        /// <summary>
        /// Inicializacia novej instancie InvalidCallAssertionException triedy.
        /// </summary>
        /// <param name="message">Sprava pre triedu System.Exception.</param>
        public InvalidCallAssertionException(string message)
            : base(message)
        {
        }
    }
}
