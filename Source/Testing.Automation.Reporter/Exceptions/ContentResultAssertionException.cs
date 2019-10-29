namespace Testing.Automation.Reporter.Exceptions
{
    using System;

    /// <summary>
    /// Vynimka pre invalid content result.
    /// </summary>
    public class ContentResultAssertionException : Exception
    {
        /// <summary>
        /// Inicializacia novej instancie ContentResultAssertionException triedy.
        /// </summary>
        /// <param name="message">Sprava pre triedu System.Exception.</param>
        public ContentResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
