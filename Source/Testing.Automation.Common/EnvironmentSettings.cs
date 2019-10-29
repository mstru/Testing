using Newtonsoft.Json;
using System;
using System.IO;

namespace Testing.Automation.Common
{
    /// <summary>
    /// Trieda obsahuje nastavenia databázy
    /// </summary>
    [Serializable]
    public class EnvironmentSettings
    {
        private readonly Settings settings;

        class Settings
        {
            public string SID { get; set; }
            public string HOST { get; set; }
            public string PORT { get; set; }
            public string USER { get; set; }
            public string PASSWORD1 { get; set; }
            public string PASSWORD2 { get; set; }
            public string APPSERVER { get; set; }
        }

        /// <summary>
        /// Meno používateĺa
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Heslo používateĺa
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Heslo používateľa 1
        /// </summary>
        public string Password1 { get; set; }

        /// <summary>
        /// Heslo používateľa 2
        /// </summary>
        public string Password2 { get; set; }

        /// <summary>
        /// Host do databázy
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Port do databázy
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// Sid do databázy
        /// </summary>
        public string Sid { get; set; }

        /// <summary>
        /// Adresa aplikacneho servera
        /// </summary>
        public string AppServer { get; set; }

        /// <summary>
        /// Inicializácia nového nastavenia do databázy s daným používateľským menom a heslom
        /// </summary>
        /// <param name="username">Meno používateĺa</param>
        /// <param name="password">Heslo používateľa</param>
        /// <param name="host">Host do databázy</param>
        /// <param name="port">Port do databázy</param>
        /// <param name="sid">Sid do databázy</param>
        /// <param name="AppServer">Adresa na aplikacny server</param>
        public EnvironmentSettings()
        {
            settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(BaseConfiguration.PathToPropertiesFile));

            Username = settings.USER;
            Password1 = settings.PASSWORD1;
            Password2 = settings.PASSWORD2;
            Host = settings.HOST;
            Port = settings.PORT;
            Sid = settings.SID;
            AppServer = settings.APPSERVER;

        }
    }
}
