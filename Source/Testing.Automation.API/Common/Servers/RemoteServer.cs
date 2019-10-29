namespace Testing.Automation.API.Common.Servers
{
    using System;
    using System.Net.Http;

    /// <summary>
    /// Testovací server pre testovanie remote server.
    /// </summary>
    public static class RemoteServer
    {
        /// <summary>
        /// Získa globálny HTTP klient, ktorý sa používa na odoslanie požiadavky.
        /// </summary>
        /// <value>HttpClient instance.</value>
        public static HttpClient GlobalClient { get; private set; }

        /// <summary>
        /// Získa hodnotu, ktora indikuje ci je server nastaveny.
        /// </summary>
        /// <value>True alebo false.</value>
        public static bool GlobalIsConfigured
        {
            get { return GlobalClient != null; }
        }

        /// <summary>
        /// Vytvorenie noveho klienta HTTP pre vzdialený server
        /// </summary>
        /// <param name="baseAddress">Base address to use for the requests.</param>
        /// <returns>HttpClient instance.</returns>
        public static HttpClient CreateNewClient(string baseAddress)
        {
            return new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };
        }

        /// <summary>
        /// Konfiguruje inštanciu klienta vzdialeného servera.
        /// </summary>
        /// <param name="baseAddress">Základná adresa pre requesty.</param>
        public static void ConfigureGlobal(string baseAddress)
        {
            if (GlobalIsConfigured)
            {
                DisposeGlobal();
            }

            GlobalClient = CreateNewClient(baseAddress);
        }

        /// <summary>
        /// Stop server.
        /// </summary>
        /// <returns>True alebo false, indikuje ci bol server uspesne zastaveny.</returns>
        public static bool DisposeGlobal()
        {
            if (GlobalClient == null)
            {
                return false;
            }

            GlobalClient.Dispose();
            GlobalClient = null;

            return true;
        }
    }
}
