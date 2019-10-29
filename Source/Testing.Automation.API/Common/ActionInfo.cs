namespace Testing.Automation.API.Common
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Trieda obsahuje informacie o vyvolanej akcii.
    /// </summary>
    /// <typeparam name="TActionResult">Typ navratovej akcie.</typeparam>
    public class ActionInfo<TActionResult>
    {
        /// <summary>
        /// Inicializacia novej instancie <see cref="ActionInfo{TActionResult}" /> triedy.
        /// </summary>
        /// <param name="actionName">Nazov akcie.</param>
        /// <param name="actionAttributes">Zoznam atributov.</param>
        /// <param name="actionResult">Hodnota navratovej akcie.</param>
        /// <param name="caughtException">Chytena vynimka pocas vykonavania akcie.</param>
        public ActionInfo(string actionName, IEnumerable<object> actionAttributes, TActionResult actionResult, Exception caughtException)
        {
            this.ActionName = actionName;
            this.ActionAttributes = actionAttributes;
            this.ActionResult = actionResult;
            this.CaughtException = caughtException;
        }

        /// <summary>
        /// Ziskanie nazvu akcie.
        /// </summary>
        /// <value>String hodnota.</value>
        public string ActionName { get; private set; }

        /// <summary>
        /// Ziskanie atributu akcie.
        /// </summary>
        /// <value>IEnumerable objektov.</value>
        public IEnumerable<object> ActionAttributes { get; private set; }

        /// <summary>
        /// Ziska navratovu hodnotu akcie.
        /// </summary>
        /// <value>Vysledok akcie je TActionResult.</value>
        public TActionResult ActionResult { get; private set; }

        /// <summary>
        /// Ziskanie chytenej vynimky pocas vykonavania akcie.
        /// </summary>
        /// <value>Vynimka.</value>
        public Exception CaughtException { get; set; }
    }
}
