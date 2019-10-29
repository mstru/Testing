namespace Testing.Automation.Reporter.Exceptions
{
    using System;

    /// <summary>
    /// Vynimka pre invalid JSON result.
    /// </summary>
    public class JsonResultAssertionException : Exception
    {
        /// <summary>
        /// Inicializacia novej instancie JSONResultAssertionException triedy.
        /// </summary>
        /// <param name="message">Sprava pre triedu System.Exception.</param>
        public JsonResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
