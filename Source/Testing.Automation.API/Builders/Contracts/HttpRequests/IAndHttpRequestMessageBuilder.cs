namespace Testing.Automation.API.Builders.Contracts.HttpRequests
{
    /// <summary>
    /// Používa sa na pridanie metódy AndAlso () do nástroja na tvorbu správ HTTP.
    /// </summary>
    public interface IAndHttpRequestMessageBuilder : IHttpRequestMessageBuilder
    {
        /// <summary>
        /// AndAlso metóda pre lepšiu čitateľnosť pri vytváraní správy HTTP.
        /// </summary>
        /// <returns>The same HTTP request message builder.</returns>
        IHttpRequestMessageBuilder AndAlso();
    }
}
