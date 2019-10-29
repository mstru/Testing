namespace Testing.Automation.Reporter.Exceptions
{
    using System;

    /// <summary>
    /// Vynimka pre invalid action return type when expecting response model.
    /// </summary>
    public class ResponseModelAssertionException : Exception
    {
        /// <summary>
        /// Inicializacia novej instancie ResponseModelAssertionException triedy.
        /// </summary>
        /// <param name="message">Sprava pre triedu System.Exception.</param>
        public ResponseModelAssertionException(string message)
            : base(message)
        {
        }
    }
}
