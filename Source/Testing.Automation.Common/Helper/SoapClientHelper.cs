using Microsoft.Web.Services3;
using System;
using System.IO;
using System.Net;
using System.Xml;

namespace Testing.Automation.Common.Helper
{
    /// <summary>
    /// Soap client pre volanie interných webových služieb nad XMatik aplikačným serverom.
    /// </summary>
    public class SoapClientHelper
    {
        class Settings
        {
            public string url { get; set; }
            public string id { get; set; }
            public string dateTimeFrom { get; set; }
            public string dateTimeTo { get; set; }
        }


        /// <summary>
        /// Metóda na zavolanie internej služby a jej metódy "StartSynchroXMOp"
        /// </summary>
        /// <param name="url">Interná adresa WS na aplikačnom serveri.</param>
        /// <param name="id">Id procesu, ktorý bude spúšťaný.</param>
        /// <param name="dateTimeFrom">Dátum od vo formáte [dd.MM.yyyy HH:mm:ss].</param>
        /// <param name="dateTimeTo">Dátum do vo formáte [dd.MM.yyyy HH:mm:ss]</param>
        public static void CallService(string url, string id, string dateTimeFrom, string dateTimeTo, ref string processStartupTime)
        {
            var settings = new Settings();

            if (url == null)
                throw new ArgumentNullException(nameof(url));
            else { settings.url = url; }

            if (id == null)
                throw new ArgumentNullException(nameof(id));
            else { settings.id = id; } 

            if (dateTimeFrom == null)
                throw new ArgumentNullException(nameof(dateTimeFrom));
            else { settings.dateTimeFrom = dateTimeFrom; }

            if (dateTimeTo == null)
                throw new ArgumentNullException(nameof(dateTimeTo));
            else { settings.dateTimeTo = dateTimeTo; }
         

            HttpWebRequest webRequest = CreateSoapRequest(settings);

            XmlDocument soapEnvelopeXml = null;

            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

            processStartupTime = System.DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");

            try
            {
                using (var response = webRequest.GetResponse())
                {
                    using (var rd = new StreamReader(response.GetResponseStream()))
                    {
                        var soapResult = rd.ReadToEnd();            
                    }    
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurred, call web service failed - " + settings.url + Environment.NewLine + ex.Message);
            }
        }

        /// <summary>
        /// Metóda na zavolanie internej služby a jej metódy "StartSynchroXMOp"
        /// </summary>
        /// <param name="url">Interná adresa WS na aplikačnom serveri.</param>
        /// <param name="id">Id procesu, ktorý bude spúšťaný.</param>
        /// <param name="dateTimeFrom">Dátum od vo formáte [dd.MM.yyyy HH:mm:ss].</param>
        /// <param name="dateTimeTo">Dátum do vo formáte [dd.MM.yyyy HH:mm:ss]</param>
        public static void CallService(string url, string id, string dateTimeFrom, string dateTimeTo, ref System.DateTime processStartupTime)
        {
            var settings = new Settings();

            if (url == null)
                throw new ArgumentNullException(nameof(url));
            else { settings.url = url; }

            if (id == null)
                throw new ArgumentNullException(nameof(id));
            else { settings.id = id; }

            if (dateTimeFrom == null)
                throw new ArgumentNullException(nameof(dateTimeFrom));
            else { settings.dateTimeFrom = dateTimeFrom; }

            if (dateTimeTo == null)
                throw new ArgumentNullException(nameof(dateTimeTo));
            else { settings.dateTimeTo = dateTimeTo; }

            XmlDocument soapEnvelopeXml = null;

            HttpWebRequest webRequest = CreateSoapRequest(settings);

            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

            processStartupTime = System.DateTime.Now;

            try
            {
                using (var response = webRequest.GetResponse())
                {
                    using (var rd = new StreamReader(response.GetResponseStream()))
                    {
                        var soapResult = rd.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurred, call web service failed - " + settings.url + Environment.NewLine + ex.Message);
            }
        }

        private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }

        /// <summary>
        /// Vytvorenie soap requestu.
        /// </summary>
        /// <param name="url">Interná adresa WS na aplikačnom serveri.</param>
        /// <returns>Nastavený web request.</returns>
        private static HttpWebRequest CreateSoapRequest(Settings settings)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(settings.url);
            webRequest.Proxy = null;
            webRequest.ContentType = "application/soap+xml; charset=utf-8";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";

            return webRequest;
        }
    }
}
