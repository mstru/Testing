namespace Testing.Automation.API
{
    using System;
    using System.Net.Http;
    using System.Web.Http;
    using Builders;
    using Builders.Contracts;
    using Builders.Contracts.Servers;
    using Builders.Controllers;
    using Builders.HttpMessages;
    using Builders.Routes;
    using Builders.Api;
    using Common.Extensions;
    using Utilities;
    using Utilities.Validators;
    using global::Testing.Automation.Common;


    /// <summary>
    /// Východiskový bod testovacieho rámca, ktorý poskytuje spôsob, ako špecifikovať funkciu ASP.NET Web API, ktorá sa má testovať.
    /// </summary>
    public static class MyWebApi
    {
        /// <summary>
        /// Predvolený hostiteľ, ktorý testy použijú.
        /// </summary>
        public static String DefaultHost { get; set; }

        /// <summary>
        /// JWT token, pre aktualizaciu treba zobrat autorizacny kluc z prehliadaca
        /// </summary>
        const string token = "custom token";

        const string contentType = "application/json; charset=utf-8";

        static MyWebApi()
        {
            IsUsingDefaultHttpConfiguration();
        }

        public static String ContentType { get; internal set; }

        /// <summary>
        /// Získa aktuálnu globálnu konfiguráciu HTTP použitú pri testovaní.
        /// </summary>
        /// <value>Instance of HttpConfiguration.</value>
        public static HttpConfiguration Configuration { get; private set; }

        /// <summary>
        /// Získa aktuálnu bázovú adresu použitú pri testovaní.
        /// </summary>
        /// <value>Instance of String.</value>
        public static Uri BaseAddress { get; internal set; }

        /// <summary>
        /// Získa aktuálny token pre autorizáciu pri testovaní.
        /// </summary>
        public static String JwtToken { get; internal set; }

        /// <summary>
        /// Je test v stave zlyhania?
        /// </summary>
        public static bool IsTestFailed { get; set; }

        /// <summary>
        /// Nastaví predvolenú hodnotu HttpConfiguration, ktorá sa použije vo všetkých testoch.
        /// </summary>
        /// <returns>HTTP configuration builder.</returns>
        public static IHttpConfigurationBuilder IsUsingDefaultHttpConfiguration()
        {
            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "API Default",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            return IsUsing(config);
        }

        /// <summary>
        /// Nastaví HttpConfiguration, ktorý bude použitý vo všetkých testoch.
        /// </summary>
        /// <param name="httpConfiguration">HttpConfiguration instance used in the testing.</param>
        /// <returns>HTTP configuration builder.</returns>
        public static IHttpConfigurationBuilder IsUsing(HttpConfiguration httpConfiguration)
        {
            DefaultHost = BaseConfiguration.GetUrlValue;
            JwtToken = token;
            ContentType = contentType;
            Configuration = httpConfiguration;
            BaseAddress = new Uri(BaseConfiguration.GetUrlValue, UriKind.Absolute);     
            return new HttpConfigurationBuilder(httpConfiguration);
        }

        /// <summary>
        /// Nastaví akciu, ktorá zaregistruje HttpConfiguration použitú vo všetkých testoch.
        /// </summary>
        /// <param name="httpConfigurationRegistration">Action to register HttpConfiguration.</param>
        /// <returns>HTTP configuration builder.</returns>
        public static IHttpConfigurationBuilder IsRegisteredWith(Action<HttpConfiguration> httpConfigurationRegistration)
        {
            var configuration = new HttpConfiguration();
            httpConfigurationRegistration(configuration);
            return IsUsing(configuration);
        }

        /// <summary>
        /// Starts a route test.
        /// </summary>
        /// <param name="httpConfiguration">Optional HttpConfiguration to use in case one is not configured globally.</param>
        /// <returns>Route test builder.</returns>
        public static IRouteTestBuilder Routes(HttpConfiguration httpConfiguration = null)
        {
            if (httpConfiguration == null)
            {
                HttpConfigurationValidator.ValidateGlobalConfiguration("routes");
                httpConfiguration = Configuration;
            }

            return new RouteTestBuilder(httpConfiguration);
        }

        /// <summary>
        /// Vyberie obslužný program správy HTTP, na ktorom bude test vykonaný.HttpMessageHandler je inštancia s predvoleným konštruktorom.
        /// </summary>
        /// <typeparam name="THandler">Instance of type HttpMessageHandler.</typeparam>
        /// <returns>Handler builder used to build the test case.</returns>
        public static IHttpMessageHandlerBuilder Handler<THandler>()
            where THandler : HttpMessageHandler
        {
            var handler = Reflection.TryCreateInstance<THandler>();
            return Handler(() => handler);
        }

        /// <summary>
        /// Vyberie obslužný program správy HTTP, na ktorom bude test vykonaný.
        /// </summary>
        /// <param name="handler">Instance of the HttpMessageHandler to use.</param>
        /// <returns>Handler builder used to build the test case.</returns>
        public static IHttpMessageHandlerBuilder Handler(HttpMessageHandler handler)
        {
            return Handler(() => handler);
        }

        /// <summary>
        /// Vyberie obslužný program správy HTTP, na ktorom bude test vykonaný.HttpMessageHandler je vytvorený pomocou konštrukčnej funkcie.
        /// </summary>
        /// <param name="construction">Construction function returning the instantiated HttpMessageHandler.</param>
        /// <returns>Handler builder used to build the test case.</returns>
        public static IHttpMessageHandlerBuilder Handler(Func<HttpMessageHandler> construction)
        {
            var handlerInstance = construction();
            return new HttpMessageHandlerTestBuilder();
        }

        /// <summary>
        /// Voľba ovládača, na ktorom sa test vykoná.Kontrolér je inštanktovaný s globálne registrovaným riešiteľom závislostí alebo s jeho predvoleným konštruktérom, ak je riešenie neúspešné
        /// </summary>
        /// <typeparam name="TController">Class inheriting ASP.NET Web API controller.</typeparam>
        /// <returns>Controller builder used to build the test case.</returns>
        public static IControllerBuilder<TController> Controller<TController>()
            where TController : ApiController
        {
            var controller = Configuration.TryResolve<TController>() ?? Reflection.TryFastCreateInstance<TController>();
            return Controller(() => controller);
        }

        /// <summary>
        /// Voľba ovládača, na ktorom sa test vykoná.
        /// </summary>
        /// <typeparam name="TController">Class inheriting ASP.NET Web API controller.</typeparam>
        /// <param name="controller">Instance of the ASP.NET Web API controller to use.</param>
        /// <returns>Controller builder used to build the test case.</returns>
        public static IControllerBuilder<TController> Controller<TController>(TController controller)
            where TController : ApiController
        {
            return Controller(() => controller);
        }

        /// <summary>
        /// Voľba ovládača, na ktorom sa test vykoná.Kontrolér je vytvorený pomocou konštrukčnej funkcie.
        /// </summary>
        /// <typeparam name="TController">Class inheriting ASP.NET Web API controller.</typeparam>
        /// <param name="construction">Construction function returning the instantiated controller.</param>
        /// <returns>Controller builder used to build the test case.</returns>
        public static IControllerBuilder<TController> Controller<TController>(Func<TController> construction)
            where TController : ApiController
        {
            var controllerInstance = construction();
            return new ControllerBuilder<TController>(controllerInstance);
        }

        /// <summary>
        /// Spustí test ASP.NET Web API. Volania voci vyvojovemu prostrediu/localhost
        /// </summary>
        public static IServer Start()
        {
            return new Api();
        }
    }
}
