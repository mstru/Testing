namespace Testing.Automation.Reporter.Exceptions
{
    using System;

    /// <summary>
    /// Vynimka pre model with errors.
    /// </summary>
    public class ModelErrorAssertionException : Exception
    {
        /// <summary>
        /// Inicializacia novej instancie ModelErrorAssertionException triedy.
        /// </summary>
        /// <param name="message">Sprava pre triedu System.Exception.</param>
        public ModelErrorAssertionException(string message)
            : base(message)
        {
        }
    }
}
