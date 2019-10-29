namespace Testing.Automation.Reporter.Exceptions
{
    using System;

    /// <summary>
    ///Vynimka pre invalid internal server error result.
    /// </summary>
    public class InternalServerErrorResultAssertionException : Exception
    {
        /// <summary>
        /// Inicializacia novej instancie InternalServerErrorResultAssertionException triedy.
        /// </summary>
        /// <param name="message">Sprava pre triedu System.Exception.</param>
        public InternalServerErrorResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
