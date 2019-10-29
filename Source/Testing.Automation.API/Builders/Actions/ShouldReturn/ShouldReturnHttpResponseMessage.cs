namespace Testing.Automation.API.Builders.Actions.ShouldReturn
{
    using System.Net.Http;
    using Contracts.HttpResponseMessages;
    using HttpMessages;

    /// <summary>
    /// Trieda obsahujúca metódy na testovanie výsledku HttpResponseMessage.
    /// </summary>
    /// <typeparam name="TActionResult">Vysledok z vyvolanej akcie ASP.NET Web API controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Testuje ze vysledok akcie je HttpResponseMessage.
        /// </summary>
        /// <returns>HTTP response message test builder.</returns>
        public IHttpResponseMessageTestBuilder HttpResponseMessage()
        {
            this.ResultOfType<HttpResponseMessage>();
            return new HttpResponseMessageTestBuilder(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                this.ActionResult as HttpResponseMessage);
        }
    }
}
