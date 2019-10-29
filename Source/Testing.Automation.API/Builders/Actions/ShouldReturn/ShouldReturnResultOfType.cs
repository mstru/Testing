namespace Testing.Automation.API.Builders.Actions.ShouldReturn
{
    using System;
    using Contracts.Models;
    using Models;

    /// <summary>
    /// Trieda obsahujúca metódy testovania návratového typu.
    /// </summary>
    /// <typeparam name="TActionResult">Vysledok z vyvolanej akcie ASP.NET Web API controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Testuje, či je výsledok činnosti daného typu.
        /// </summary>
        /// <param name="returnType">Ocakavany typ odpovede.</param>
        /// <returns>Response model details test builder.</returns>
        public IModelDetailsTestBuilder<TActionResult> ResultOfType(Type returnType)
        {
            this.ValidateActionReturnType(returnType, true, true);
            return new ModelDetailsTestBuilder<TActionResult>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                this.ActionResult);
        }

        /// <summary>
        /// Testuje, či je výsledok činnosti poskytnutého generického typu.
        /// </summary>
        /// <typeparam name="TResponseModel">Ocakavany typ odpovede.</typeparam>
        /// <returns>Response model test builder.</returns>
        public IModelDetailsTestBuilder<TActionResult> ResultOfType<TResponseModel>()
        {
            this.ValidateActionReturnType<TResponseModel>(true);
            return new ModelDetailsTestBuilder<TActionResult>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                this.ActionResult);
        }

        private TReturnObject GetReturnObject<TReturnObject>()
            where TReturnObject : class 
        {
            this.ValidateActionReturnType<TReturnObject>(true);
            return this.ActionResult as TReturnObject;
        }
    }
}
