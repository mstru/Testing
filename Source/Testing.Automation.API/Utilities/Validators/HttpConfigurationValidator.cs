namespace Testing.Automation.API.Utilities.Validators
{
    /// <summary>
    /// Validates HTTP configuration.
    /// </summary>
    public static class HttpConfigurationValidator
    {
        /// <summary>
        /// Validates global configuration whether it is null or not and gives appropriate exception message if not.
        /// </summary>
        /// <param name="testCase">Name of the test case to use in the message.</param>
        public static void ValidateGlobalConfiguration(string testCase)
        {
            CommonValidator.CheckForNullReference(
                MyWebApi.Configuration,
                string.Format("'IsUsing' method should be called before testing {0} or HttpConfiguration should be provided. MyWebApi must be configured and HttpConfiguration", testCase));
        }
    }
}
