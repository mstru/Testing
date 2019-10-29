namespace Testing.Automation.API.Builders.Base
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Web.Http;
    using Contracts.Base;
    using Utilities.Validators;

    /// <summary>
    /// Zakladna trieda pre vsetky test builders.
    /// </summary>
    public abstract class BaseTestBuilder : IBaseTestBuilder
    {
        private ApiController controller;

        /// <summary>
        /// Inicializacia novej instancie <see cref="BaseTestBuilder" /> triedy.
        /// </summary>
        /// <param name="controller">Controller, na ktorom sa bude testovať.</param>
        /// <param name="controllerAttributes">Zoznam atributov.</param>
        protected BaseTestBuilder(
            ApiController controller,
            IEnumerable<object> controllerAttributes = null)
        {
            this.Controller = controller;
            this.ControllerLevelAttributes = controllerAttributes;
        }

        /// <summary>
        /// Ziska controller, ktory je testovany.
        /// </summary>
        /// <value>Controller on which the action will be tested.</value>
        internal ApiController Controller
        {
            get
            {
                return this.controller;
            }

            private set
            {
                CommonValidator.CheckForNullReference(value, errorMessageName: "Controller");
                this.controller = value;
            }
        }

        internal IEnumerable<object> ControllerLevelAttributes { get; private set; }

        /// <summary>
        /// Ziska controller, ktory je testovany.
        /// </summary>
        /// <returns>ASP.NET Web API controller, ktory je testovany.</returns>
        public ApiController AndProvideTheController()
        {
            return this.Controller;
        }

        /// <summary>
        /// Získanie request HTTP, pomocou ktorej sa bude testovať.
        /// </summary>
        /// <returns>HttpRequestMessage z testovaneho controller.</returns>
        public HttpRequestMessage AndProvideTheHttpRequestMessage()
        {
            return this.Controller.Request;
        }

        /// <summary>
        /// Získanie request HTTP, pomocou ktorej sa bude testovať.
        /// </summary>
        /// <returns>HttpConfiguration z testovaneho controller.</returns>
        public HttpConfiguration AndProvideTheHttpConfiguration()
        {
            return this.Controller.Configuration;
        }

        /// <summary>
        /// Ziskanie atributov na testovanom controller
        /// </summary>
        /// <returns>IEnumerable reprezentacia objektu, ktorý reprezentuje atribúty alebo null, ak žiadne atribúty neboli najdene.</returns>
        public IEnumerable<object> AndProvideTheControllerAttributes()
        {
            return this.ControllerLevelAttributes;
        }
    }
}
