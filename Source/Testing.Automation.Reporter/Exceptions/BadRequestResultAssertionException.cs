namespace Testing.Automation.Reporter.Exceptions
{
    using System;

    /// <summary>
    /// Vynimka pre bad request result.
    /// </summary>
    public class BadRequestResultAssertionException : Exception
    {
        /// <summary>
        /// Inicializacia novej instancie BadRequestResultAssertionException triedy.
        /// </summary>
        /// <param name="message">Sprava pre triedu System.Exception.</param>
        public BadRequestResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
