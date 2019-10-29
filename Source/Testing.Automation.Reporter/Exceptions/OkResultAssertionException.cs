namespace Testing.Automation.Reporter.Exceptions
{
    using System;

    /// <summary>
    /// Vynimka pre invalid ok result.
    /// </summary>
    public class OkResultAssertionException : Exception
    {
        /// <summary>
        /// Inicializacia novej instancie OkResultAssertionException triedy.
        /// </summary>
        /// <param name="message">Sprava pre triedu System.Exception.</param>
        public OkResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
