namespace Testing.Automation.Reporter.Exceptions
{
    using System;

    /// <summary>
    /// Vynimka pre invalid URI validation.
    /// </summary>
    public class RedirectResultAssertionException : Exception
    {
        /// <summary>
        /// Inicializacia novej instancie RedirectResultAssertionException triedy.
        /// </summary>
        /// <param name="message">Sprava pre triedu System.Exception.</param>
        public RedirectResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
