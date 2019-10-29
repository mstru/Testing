namespace Testing.Automation.API.Utilities.Validators
{
    using System;
    using System.Linq;
    using Builders.Uris;

    /// <summary>
    /// Trieda validátora obsahujúca logiku validácie URI.
    /// </summary>
    public static class LocationValidator
    {
        /// <summary>
        /// Overenie URI poskytnutého ako reťazec.
        /// </summary>
        /// <param name="location">Expected URI as string.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        /// <returns>Valid Uri created from the provided string.</returns>
        public static Uri ValidateAndGetWellFormedUriString(
            string location,
            Action<string, string, string> failedValidationAction)
        {
            Uri result;
            //if (!Uri.IsWellFormedUriString(location, UriKind.RelativeOrAbsolute))
            if (!Uri.TryCreate(location, UriKind.RelativeOrAbsolute, out result))
            {
                failedValidationAction(
                    "location",
                    "to be URI valid",
                    string.Format("instead received {0}", location));
            }

            return new Uri(location, UriKind.RelativeOrAbsolute);
        }

        /// <summary>
        /// Overenie Uri z výsledku činnosti obsahujúce jeden.
        /// </summary>
        /// <param name="actionResult">Action result with Uri.</param>
        /// <param name="location">Expected Uri.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void ValidateUri(
            dynamic actionResult,
            Uri location,
            Action<string, string, string> failedValidationAction)
        {
            RuntimeBinderValidator.ValidateBinding(() =>
            {
                var actualLocation = (Uri)actionResult.Location;
                if (location != actualLocation)
                {
                    failedValidationAction(
                        "location",
                        string.Format("to be {0}", location.OriginalString),
                        string.Format("instead received {0}", actualLocation.OriginalString));
                }
            });
        }

        /// <summary>
        /// Overenie URI pomocou UriTestBuilder.
        /// </summary>
        /// <param name="actionResult">Dynamic representation of action result.</param>
        /// <param name="uriTestBuilder">UriTestBuilder for validation.</param>
        /// <param name="failedValidationAction">Action to execute, if the validation fails.</param>
        public static void ValidateLocation(
            dynamic actionResult,
            Action<MockedUriTestBuilder> uriTestBuilder,
            Action<string, string, string> failedValidationAction)
        {
            RuntimeBinderValidator.ValidateBinding(() =>
            {
                var actualUri = actionResult.Location as Uri;

                var newUriTestBuilder = new MockedUriTestBuilder();
                uriTestBuilder(newUriTestBuilder);
                var expectedUri = newUriTestBuilder.GetMockedUri();

                var validations = newUriTestBuilder.GetMockedUriValidations();
                if (validations.Any(v => !v(expectedUri, actualUri)))
                {
                    failedValidationAction(
                        "URI",
                        "to equal the provided one",
                        "was in fact different");
                }
            });
        }
    }
}
