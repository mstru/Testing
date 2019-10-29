namespace Testing.Automation.API.Utilities.Validators
{
    using System;
    using global::Testing.Automation.Reporter.Exceptions;
    using Microsoft.CSharp.RuntimeBinder;

    /// <summary>
    /// Trieda validátora obsahujúca dynamickú akciu vyvoláva logiku overenia.
    /// </summary>
    public static class RuntimeBinderValidator
    {
        /// <summary>
        /// Overenie akcie výzvy pre RuntimeBinderException.
        /// </summary>
        /// <param name="action">Action to validate.</param>
        public static void ValidateBinding(Action action)
        {
            try
            {
                action();
            }
            catch (RuntimeBinderException ex)
            {
                var fullPropertyName = ex.Message.Split('\'')[3];
                throw new InvalidCallAssertionException(string.Format(
                    "Expected action result to contain a '{0}' property to test, but in fact such property was not found.",
                    fullPropertyName));
            }
        }
    }
}
