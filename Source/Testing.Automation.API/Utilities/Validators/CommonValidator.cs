namespace Testing.Automation.API.Utilities.Validators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common.Extensions;
    using global::Testing.Automation.Reporter.Exceptions;

    /// <summary>
    /// Validator class containing common validation logic.
    /// </summary>
    public static class CommonValidator
    {

        internal static bool ValidateBinding(Action action)
        {
            try
            {
                action();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
     
        internal static T CheckNotNull<T>(this T value, string argumentName, string errorMessage = null)
        {
            if (value == null)
                throw new ArgumentNullException(argumentName, errorMessage);

            return value;
        }


        internal static string CheckNotNullOrEmpty(this string value, string argumentName, string errorMessage = null)
        {
            if (value == null)
                throw new ArgumentNullException(argumentName, errorMessage);
            if (value == string.Empty)
                throw new ArgumentException(ConcatMessage("Should not be empty string.", errorMessage), argumentName);

            return value;
        }


        internal static string CheckNotNullOrWhitespace(this string value, string argumentName, string errorMessage = null)
        {
            if (value == null)
                throw new ArgumentNullException(argumentName, errorMessage);
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(ConcatMessage("Should not be empty string or whitespace.", errorMessage), argumentName);

            return value;
        }


        internal static IEnumerable<T> CheckNotNullOrEmpty<T>(this IEnumerable<T> collection, string argumentName, string errorMessage = null)
        {
            if (collection == null)
                throw new ArgumentNullException(argumentName, errorMessage);
            if (!collection.Any())
                throw new ArgumentException(ConcatMessage("Collection should contain at least one element.", errorMessage), argumentName);

            return collection;
        }


        internal static T CheckNotEquals<T>(this T value, string argumentName, T invalidValue, string errorMessage = null)
            where T : struct
        {
            if (Equals(value, invalidValue))
                throw new ArgumentException(ConcatMessage($"Invalid {typeof(T).FullName} value: {value}. Should not equal to: {invalidValue}.", errorMessage), argumentName);

            return value;
        }


        internal static T CheckGreaterOrEqual<T>(this T value, string argumentName, T checkValue, string errorMessage = null)
            where T : struct, IComparable<T>
        {
            if (value.CompareTo(checkValue) < 0)
                throw new ArgumentOutOfRangeException(argumentName, value, ConcatMessage($"Invalid {typeof(T).FullName} value: {value}. Should be greater or equal to: {checkValue}.", errorMessage));

            return value;
        }


        internal static T CheckLessOrEqual<T>(this T value, string argumentName, T checkValue, string errorMessage = null)
            where T : struct, IComparable<T>
        {
            if (value.CompareTo(checkValue) > 0)
                throw new ArgumentOutOfRangeException(argumentName, value, ConcatMessage($"Invalid {typeof(T).FullName} value: {value}. Should be less or equal to: {checkValue}.", errorMessage));

            return value;
        }


        internal static int CheckIndexNonNegative(this int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index), index, "Index was out of range. Must be non-negative.");

            return index;
        }


        internal static Type CheckIs<T>(this Type value, string argumentName, string errorMessage = null)
        {
            value.CheckNotNull(argumentName);

            Type expectedType = typeof(T);

            if (!expectedType.IsAssignableFrom(value))
                throw new ArgumentException(ConcatMessage($"{value.FullName} type should be assignable to {expectedType.FullName}.", errorMessage), argumentName);

            return value;
        }


        private static string ConcatMessage(string primaryMessage, string secondaryMessage)
        {
            return string.IsNullOrEmpty(secondaryMessage)
                ? primaryMessage
                : $"{primaryMessage} {secondaryMessage}";
        }


        /// <summary>
        /// Validates object for null reference.
        /// </summary>
        /// <param name="value">Object to be validated.</param>
        /// <param name="errorMessageName">Name of the parameter to be included in the error message.</param>
        public static void CheckForNullReference(
            object value,
            string errorMessageName = "Value")
        {
            if (value == null)
            {
                throw new NullReferenceException(string.Format("{0} cannot be null.", errorMessageName));
            }
        }


        /// <summary>
        /// Validates string for null reference or whitespace.
        /// </summary>
        /// <param name="value">String to be validated.</param>
        /// <param name="errorMessageName">Name of the parameter to be included in the error message.</param>
        public static void CheckForNotWhiteSpaceString(
            string value,
            string errorMessageName = "Value")
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new NullReferenceException(string.Format("{0} cannot be null or white space.", errorMessageName));
            }
        }


        /// <summary>
        /// Validates whether the provided value is not null or equal to the type's default value.
        /// </summary>
        /// <typeparam name="T">Type of the provided value.</typeparam>
        /// <param name="value">Value to be validated.</param>
        /// <param name="errorMessage">Error message if the validation fails.</param>
        public static void CheckForEqualityWithDefaultValue<T>(T value, string errorMessage)
        {
            if (CheckForDefaultValue(value))
            {
                throw new InvalidOperationException(errorMessage);
            }
        }


        /// <summary>
        /// Validated whether a non-null exception is provided and throws ActionCallAssertionException with proper message.
        /// </summary>
        /// <param name="exception">Exception to be validated.</param>
        public static void CheckForException(Exception exception)
        {
            if (exception != null)
            {
                var message = FormatExceptionMessage(exception.Message);

                var exceptionAsAggregateException = exception as AggregateException;
                if (exceptionAsAggregateException != null)
                {
                    var innerExceptions = exceptionAsAggregateException
                        .InnerExceptions
                        .Select(ex => 
                            string.Format("{0}{1}", ex.GetName(), FormatExceptionMessage(ex.Message)));

                    message = string.Format(" (containing {0})", string.Join(", ", innerExceptions));
                }

                throw new InvalidCallAssertionException(string.Format(
                    "{0}{1} was thrown but was not caught or expected.",
                    exception.GetType().ToFriendlyTypeName(),
                    message));
            }
        }


        /// <summary>
        /// Validates that two objects are equal using the Equals method.
        /// </summary>
        /// <typeparam name="T">Type of the objects.</typeparam>
        /// <param name="expected">Expected object.</param>
        /// <param name="actual">Actual object.</param>
        /// <returns>True or false.</returns>
        public static bool CheckEquality<T>(T expected, T actual)
        {
            return expected.Equals(actual);
        }


        /// <summary>
        /// Validates whether object equals the default value for its type.
        /// </summary>
        /// <typeparam name="TValue">Type of the value.</typeparam>
        /// <param name="value">Object to test.</param>
        /// <returns>True or false.</returns>
        public static bool CheckForDefaultValue<TValue>(TValue value)
        {
            return EqualityComparer<TValue>.Default.Equals(value, default(TValue));
        }


        /// <summary>
        /// Validates whether type can be null.
        /// </summary>
        /// <param name="type">Type to check.</param>
        public static void CheckIfTypeCanBeNull(Type type)
        {
            bool canBeNull = !type.IsValueType || (Nullable.GetUnderlyingType(type) != null);
            if (!canBeNull)
            {
                throw new InvalidCallAssertionException(string.Format(
                    "{0} cannot be null.",
                    type.ToFriendlyTypeName()));
            }
        }


        private static string FormatExceptionMessage(string message)
        {
            return string.IsNullOrWhiteSpace(message)
                 ? string.Empty
                 : string.Format(" with '{0}' message", message);
        }
    }
}
