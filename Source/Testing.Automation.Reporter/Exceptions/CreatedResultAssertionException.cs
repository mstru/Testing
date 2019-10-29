namespace Testing.Automation.Reporter.Exceptions
{
    using System;

    /// <summary>
    /// Vynimka pre invalid created result.
    /// </summary>
    public class CreatedResultAssertionException : Exception
    {
        /// <summary>
        /// Inicializacia novej instancie CreatedResultAssertionException triedy.
        /// </summary>
        /// <param name="message">Sprava pre triedu System.Exception.</param>
        public CreatedResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
