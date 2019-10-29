namespace Testing.Automation.API.Builders.Contracts.HttpResponseMessages
{
    /// <summary>
    /// Používa sa na pridanie metódy AndAlso () k odpovedi HTTP s testami času odozvy.
    /// </summary>
    public interface IAndHttpHandlerResponseMessageWithTimeTestBuilder
        : IHttpHandlerResponseMessageWithTimeTestBuilder
    {
        /// <summary>
        /// AndAlso metóda pre lepšiu čitateľnosť pri reťazení správy HTTP odpovede s testami času odozvy.
        /// </summary>
        /// <returns>The same HTTP response message test builder.</returns>
        IHttpHandlerResponseMessageWithTimeTestBuilder AndAlso();
    }
}
