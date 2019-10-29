namespace Testing.Automation.Reporter.Exceptions
{
    using System;

    /// <summary>
    /// Vynimka pre neplatny atribut result.
    /// </summary>
    public class AttributeAssertionException : Exception
    {
        /// <summary>
        /// Inicializacia novej instancie AttributeAssertionException triedy.
        /// </summary>
        /// <param name="message">Sprava pre triedu System.Exception.</param>
        public AttributeAssertionException(string message)
            : base(message)
        {
        }
    }
}
