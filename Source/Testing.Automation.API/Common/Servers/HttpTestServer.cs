namespace Testing.Automation.API.Common.Servers
{
    using System.Net.Http;
    using System.Web.Http;

    /// <summary>
    /// Http server pre testing.
    /// </summary>
    public static class HttpTestServer
    {
        /// <summary>
        /// Získa globálny HTTP server použitý pri testovaní.
        /// </summary>
        /// <value>HttpServer instancia.</value>
        public static HttpServer GlobalServer { get; private set; }

        /// <summary>
        /// Získa globálny HTTP klient, ktorý sa používa na odoslanie požiadavky.
        /// </summary>
        /// <value>HttpClient instance.</value>
        public static HttpClient GlobalClient { get; private set; }

        /// <summary>
        /// Získate hodnotu, ktorá indikuje, či bol server HTTP spustený a počúva.
        /// </summary>
        /// <value>True alebo false.</value>
        public static bool GlobalIsStarted
        {
            get
            {
                return GlobalClient != null && GlobalServer != null;
            }
        }

        /// <summary>
        /// Vytvorí noveho HTTP klienta pre server.
        /// </summary>
        /// <param name="httpConfiguration">HTTP konfiguráciu, ktorá sa má použiť pre nase requesty.</param>
        /// <returns>HttpClient instacia.</returns>
        public static HttpClient CreateNewClient(HttpConfiguration httpConfiguration)
        {
            var httpServer = new HttpServer(httpConfiguration);
            return new HttpClient(httpServer);
        }

        /// <summary>
        /// Spustí globálnu inštanciu servera HTTP vo formáte singleton.
        /// </summary>
        /// <param name="httpConfiguration">HTTP konfiguráciu, ktorá sa má použiť pre nase requesty.</param>
        public static void StartGlobal(HttpConfiguration httpConfiguration)
        {
            if (GlobalIsStarted)
            {
                StopGlobal();
            }

            GlobalServer = new HttpServer(httpConfiguration);
            GlobalClient = new HttpClient(GlobalServer, true);
        }

        /// <summary>
        /// Stop HTTP server.
        /// </summary>
        /// <returns>True alebo false, indikuje ci bol server uspesne zastaveny.</returns>
        public static bool StopGlobal()
        {
            if (GlobalServer == null || GlobalClient == null)
            {
                return false;
            }

            GlobalClient.Dispose();

            GlobalClient = null;
            GlobalServer = null;

            return true;
        }
    }
}
