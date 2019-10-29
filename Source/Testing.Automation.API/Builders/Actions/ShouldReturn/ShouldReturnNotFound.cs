namespace Testing.Automation.API.Builders.Actions.ShouldReturn
{
    using System.Web.Http.Results;
    using Contracts.Base;

    /// <summary>
    /// Trieda obsahujúca metódy testovania NotFoundResult.
    /// </summary>
    /// <typeparam name="TActionResult">Vysledok z vyvolanej akcie ASP.NET Web API controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Testuje ci je vysledkom cinnosti, vysledok NotFoundResult.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        public IBaseTestBuilderWithActionResult<TActionResult> NotFound()
        {
            this.ResultOfType<NotFoundResult>();
            return this.NewAndProvideTestBuilder();
        }
    }
}
