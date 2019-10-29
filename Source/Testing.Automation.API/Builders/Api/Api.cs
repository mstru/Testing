namespace Testing.Automation.API.Builders.Api
{
    using System;
    using System.Web.Http;
    using Common.Servers;
    using Contracts.Servers;
    using Utilities.Validators;

    /// <summary>
    /// Trieda poskytuje moznosti spustenia a zastavenia servera HTTP, ako aj spracovanie Http requesty.
    /// </summary>
    public class Api : IServer
    {
        /// <summary>
        /// Spusti novy HTTP server.
        /// </summary>
        /// <param name="httpConfiguration">Volitelna konfiguracia protokolu HTTP. Ak nie je poskytnuta ziadna konfiguracia, namiesto nej sa pouzije globalna.</param>
        /// <returns>Server builder.</returns>
        public IApiBuilder Starts(HttpConfiguration httpConfiguration = null)
        {
            if (httpConfiguration == null)
            {
                HttpConfigurationValidator.ValidateGlobalConfiguration("server pipeline");
                httpConfiguration = MyWebApi.Configuration;
            }

            HttpTestServer.StartGlobal(httpConfiguration);
            return this.Working();
        }

        /// <summary>
        /// Spusti novy OWIN server.
        /// </summary>
        /// <typeparam name="TStartup">OWIN startup class to use.</typeparam>
        /// <param name="port">Sietovy port na ktorom server pocuva prichadzajuce poziadavky.</param>
        /// <param name="host">Sietovy host na ktorom server pocuva prichadzajuce poziadavky.</param>
        /// <returns>Server builder.</returns>
        public IApiBuilder Starts<TStartup>()
        {
            OwinTestServer.StartGlobal<TStartup>(this.GetStartOptions(MyWebApi.DefaultHost));
            return this.Working();
        }

        /// <summary>
        /// Konfiguracia vzdialeneho servera.
        /// </summary>
        /// <param name="baseAddress">Zakladna adresa pre ziadosti.</param>
        /// <returns>Server builder.</returns>
        public IApiBuilder IsLocatedAt(string baseAddress)
        {
            RemoteServer.ConfigureGlobal(baseAddress);
            return this.WorkingRemotely();
        }

        /// <summary>
        /// Zastavi vsetky spustene serveri HTTP aj OWIN.
        /// </summary>
        public void Stops()
        {
            var httpServerStoppedSuccessfully = HttpTestServer.StopGlobal();
            var owinServerStoppedSuccessfully = OwinTestServer.StopGlobal();

            if (!httpServerStoppedSuccessfully && !owinServerStoppedSuccessfully)
            {
                throw new InvalidOperationException("There are no running test servers to stop. Calling WebApiTesting.Server().Stops() should be done only after MyWebApi.Server.Starts() is invoked.");
            }
        }

        /// <summary>
        /// Spracováva požiadavky HTTP na globálne spustených serveroch HTTP alebo OWIN. Ak je spustený globálny server OWIN, bude použitý. Iba ak metóda nekontroluje, či má byť použitý globálny HTTP server. Ak sa takáto metóda nenájde, vytvorí sa nová inštancia HTTP servera s globálnou konfiguráciou HTTP.
        /// </summary>
        /// <returns>Server builder to set specific HTTP requests.</returns>
        public IApiBuilder Working()
        {
            if (OwinTestServer.GlobalIsStarted)
            {
                return new ApiTestBuilder(OwinTestServer.GlobalClient);
            }

            if (HttpTestServer.GlobalIsStarted)
            {
                return new ApiTestBuilder(HttpTestServer.GlobalClient, transformRequest: true);
            }

            if (MyWebApi.Configuration != null)
            {
                return this.Working(MyWebApi.Configuration);
            }

            throw new InvalidOperationException("No test servers are started or could be started for this particular test case. Either call WebApiTesting.Server().Starts() to start a new test server or provide global or test specific HttpConfiguration.");
        }

        /// <summary>
        /// Spustí nový server HTTP na spracovanie žiadosti.
        /// </summary>
        /// <param name="httpConfiguration">Konfigurácia protokolu HTTP, ktorá sa má použiť pri testovaní.</param>
        /// <returns>Server builder to set specific HTTP requests.</returns>
        public IApiBuilder Working(HttpConfiguration httpConfiguration)
        {
            return new ApiTestBuilder(HttpTestServer.CreateNewClient(httpConfiguration), transformRequest: true, disposeServer: true);
        }

        /// <summary>
        /// Spustí nový server OWIN na spracovanie žiadosti.
        /// </summary>
        /// <typeparam name="TStartup">OWIN startup class to use.</typeparam>
        /// <param name="port">Sietovy port na ktorom server pocuva prichadzajuce poziadavky.</param>
        /// <param name="host">Sietovy host na ktorom server pocuva prichadzajuce poziadavky.</param>
        /// <returns>Server builder to set specific HTTP requests.</returns>
        public IApiBuilder Working<TStartup>()
        {
            var options = this.GetStartOptions(MyWebApi.DefaultHost);
            var server = OwinTestServer.CreateNewServer<TStartup>(options);
            return new ApiTestBuilder(server.HttpClient, disposeServer: true, server: server);
        }

        /// <summary>
        /// Spracováva žiadosť HTTP na globálne nakonfigurovanom vzdialenom HTTP serveri.
        /// </summary>
        /// <returns>Server builder to set specific HTTP requests.</returns>
        public IApiBuilder WorkingRemotely()
        {
            if (RemoteServer.GlobalIsConfigured)
            {
                return new ApiTestBuilder(RemoteServer.GlobalClient);
            }

            throw new InvalidOperationException("No remote server is configured for this particular test case. Either call MyWebApi.Server().IsLocatedAt() to configure a new remote server or provide test specific base address.");
        }

        /// <summary>
        /// Spracováva žiadosť HTTP na vzdialenom HTTP serveri umiestnenom na poskytovanej bazovej adrese.
        /// </summary>
        /// <param name="baseAddress">Zakladna adresa, ktoru requesty pouziju.</param>
        /// <returns>Server builder to set specific HTTP requests.</returns>
        public IApiBuilder WorkingRemotely(string baseAddress)
        {
            return new ApiTestBuilder(RemoteServer.CreateNewClient(baseAddress), disposeServer: true);
        }

        private string GetStartOptions(string host)
        {
            return string.Format("{0}", host);
        }
    }
}
