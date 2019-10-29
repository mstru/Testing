using OpenQA.Selenium;
using System;

namespace Testing.Automation.Reporter.Exceptions
{
    /// <summary>
    /// Trieda pomáha pri logovaní chyby.
    /// </summary>
    public class ErrorDetail
    {
        /// <summary>
        /// Inicializácia novej inštancie <see cref="ErrorDetail" /> triedy.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="exception">The exception.</param>
        public ErrorDetail(DateTime dateTime, Exception exception, string url, string customMessage = null)
        {
            this.DateTime = dateTime;
            this.Exception = exception;
            this.CustomMessage = customMessage;
            this.Url = url;
        }

        public ErrorDetail(Screenshot screenshot, DateTime dateTime, string url, Exception exception, string customMessage = null)
        {
            this.Screenshot = screenshot;
            this.DateTime = dateTime;
            this.Url = url;
            this.Exception = exception;
            this.CustomMessage = customMessage;
        }

        /// <summary>
        /// Získanie a nastavenie snímky obrazovky.
        /// </summary>
        /// <value>
        /// Snímka.
        /// </value>
        public Screenshot Screenshot { get; set; }

        /// <summary>
        /// Získanie a nastavenie datetime.
        /// </summary>
        /// <value>
        /// datetime.
        /// </value>
        public System.DateTime DateTime { get; set; }

        /// <summary>
        /// Získanie a nastavenie chyby.
        /// </summary>
        /// <value>
        /// Chyba.
        /// </value>
        public System.Exception Exception { get; set; }

        /// <summary>
        /// Vlastna hlaska
        /// </summary>
        public string CustomMessage { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }
    }
}
