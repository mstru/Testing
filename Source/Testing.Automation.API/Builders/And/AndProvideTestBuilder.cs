namespace Testing.Automation.API.Builders.And
{
    using System;
    using System.Web.Http;
    using Base;

    /// <summary>
    /// Poskytuje informácie o akciach.
    /// </summary>
    public class AndProvideTestBuilder : BaseTestBuilderWithCaughtException
    {
        /// <summary>
        /// Inicializacia novej instancie <see cref="AndProvideTestBuilder" /> triedy.
        /// </summary>
        /// <param name="controller">Controller na ktorom bude cinnost testovana.</param>
        /// <param name="actionName">Nazov testovanej akcie.</param>
        /// <param name="caughtException">Chyba vynimky.</param>
        public AndProvideTestBuilder(ApiController controller, string actionName, Exception caughtException)
            : base(controller, actionName, caughtException)
        {
        }
    }
}
