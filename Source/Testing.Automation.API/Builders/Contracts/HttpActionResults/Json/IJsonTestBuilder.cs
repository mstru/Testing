﻿namespace Testing.Automation.API.Builders.Contracts.HttpActionResults.Json
{
    using System;
    using System.Text;
    using Models;
    using Newtonsoft.Json;

    /// <summary>
    /// Used for testing JSON results.
    /// </summary>
    public interface IJsonTestBuilder : IBaseResponseModelTestBuilder
    {
        /// <summary>
        /// Tests whether JSON result has the default UTF8 encoding.
        /// </summary>
        /// <returns>The same JSON test builder.</returns>
        IAndJsonTestBuilder WithDefaultEncoding();

        /// <summary>
        /// Tests whether JSON result has the provided encoding.
        /// </summary>
        /// <param name="encoding">Expected encoding to test with.</param>
        /// <returns>The same JSON test builder.</returns>
        IAndJsonTestBuilder WithEncoding(Encoding encoding);

        /// <summary>
        /// Tests whether JSON result has the default JSON serializer settings.
        /// </summary>
        /// <returns>The same JSON test builder.</returns>
        IAndJsonTestBuilder WithDefaulJsonSerializerSettings();

        /// <summary>
        /// Tests whether JSON result has the provided JSON serializer settings.
        /// </summary>
        /// <param name="jsonSerializerSettings">Expected JSON serializer settings to test with.</param>
        /// <returns>The same JSON test builder.</returns>
        IAndJsonTestBuilder WithJsonSerializerSettings(JsonSerializerSettings jsonSerializerSettings);

        /// <summary>
        /// Tests whether JSON result has JSON serializer settings by using builder.
        /// </summary>
        /// <param name="jsonSerializerSettingsBuilder">Builder for creating JSON serializer settings.</param>
        /// <returns>The same JSON test builder.</returns>
        IAndJsonTestBuilder WithJsonSerializerSettings(
            Action<IJsonSerializerSettingsTestBuilder> jsonSerializerSettingsBuilder);
    }
}
