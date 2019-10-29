﻿namespace Testing.Automation.API.Builders.Contracts.HttpResponseMessages
{
    /// <summary>
    /// Používa sa na pridanie metódy AndAlso () do testov správ HTTP odpovede.
    /// </summary>
    public interface IAndHttpResponseMessageTestBuilder : IHttpResponseMessageTestBuilder
    {
        /// <summary>
        /// AndAlso metóda pre lepšiu čitateľnosť pri reťaze testov správ HTTP odpovede.
        /// </summary>
        /// <returns>The same HTTP response message test builder.</returns>
        IHttpResponseMessageTestBuilder AndAlso();
    }
}