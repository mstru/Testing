namespace Testing.Automation.API.Common.Servers
{
    using System;
    using System.Net.Http;
    using Microsoft.Owin.Testing;

    /// <summary>
    /// OWIN server pre testing.
    /// </summary>
    public static class OwinTestServer
    {
        /// <summary>
        /// Získa globálny OWIN server použitý pri testovaní.
        /// </summary>
        /// <value>Test server instance.</value>
        public static TestServer GlobalServer { get; private set; }

        /// <summary>
        /// Získa globálny OWIN klient, ktorý sa používa na odoslanie požiadavky.
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
        /// Vytvorí noveho server..
        /// </summary>
        /// <typeparam name="TStartup">OWIN startup trieda.</typeparam>
        /// <param name="baseAddress">Zakladna adresa pre requesty.</param>
        /// <returns>OWIN test server.</returns>
        public static TestServer CreateNewServer<TStartup>(string baseAddress)
        {
            var server = TestServer.Create<TStartup>();
            server.BaseAddress = new Uri(baseAddress);
            return server;
        }

        /// <summary>
        /// Spustí globálnu inštanciu servera OWIN vo formáte singleton.
        /// </summary>
        /// <typeparam name="TStartup">OWIN startup class to use.</typeparam>
        /// <param name="baseAddress">Base address to use for the requests.</param>
        public static void StartGlobal<TStartup>(string baseAddress)
        {
            if (GlobalIsStarted)
            {
                StopGlobal();
            }

            GlobalServer = CreateNewServer<TStartup>(baseAddress);
            GlobalClient = GlobalServer.HttpClient;
        }

        /// <summary>
        /// Stop OWIN server.
        /// </summary>
        /// <returns>True alebo false, indikuje ci bol server uspesne zastaveny</returns>
        public static bool StopGlobal()
        {
            if (GlobalServer == null || GlobalClient == null)
            {
                return false;
            }

            GlobalClient.Dispose();
            GlobalServer.Dispose();

            GlobalClient = null;
            GlobalServer = null;

            return true;
        }
    }
}
