namespace Testing.Automation.Reporter.Exceptions
{
    using System;

    /// <summary>
    /// Vynimka pre controller unresolved dependencies.
    /// </summary>
    public class UnresolvedDependenciesException : Exception
    {
        /// <summary>
        /// Inicializacia novej instancie UnresolvedDependenciesException triedy.
        /// </summary>
        /// <param name="message">Sprava pre triedu System.Exception.</param>
        public UnresolvedDependenciesException(string message)
            : base(message)
        {
        }
    }
}
