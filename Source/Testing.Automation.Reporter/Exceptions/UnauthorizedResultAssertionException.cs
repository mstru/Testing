namespace Testing.Automation.Reporter.Exceptions
{
    using System;

    /// <summary>
    /// Vynimka pre invalid unauthorized result when authentication header challenges do not match.
    /// </summary>
    public class UnauthorizedResultAssertionException : Exception
    {
        /// <summary>
        /// Inicializacia novej instancie UnauthorizedResultAssertionException triedy.
        /// </summary>
        /// <param name="message">Sprava pre triedu System.Exception.</param>
        public UnauthorizedResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
