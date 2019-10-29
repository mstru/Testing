namespace Testing.Automation.Reporter.Exceptions
{
    using System;

    /// <summary>
    /// Vynimka pre invalid route test.
    /// </summary>
    public class RouteAssertionException : Exception
    {
        /// <summary>
        /// Inicializacia novej instancie RouteAssertionException triedy.
        /// </summary>
        /// <param name="message">Sprava pre triedu System.Exception.</param>
        public RouteAssertionException(string message)
            : base(message)
        {
        }
    }
}
