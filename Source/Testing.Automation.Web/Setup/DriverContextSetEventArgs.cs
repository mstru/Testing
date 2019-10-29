using OpenQA.Selenium;
using System;

namespace Testing.Automation.Web.Setup
{
    public class DriverContextSetEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes novej inštancie <see cref="DriverContextSetEventArgs" /> triedy.
        /// </summary>
        /// <param name="options">Možnosti.</param>
        public DriverContextSetEventArgs(DriverOptions options)
        {
            this.DriverOptions = options;
        }

        /// <summary>
        /// Získanie aktuálne možnosti capabilities.
        /// </summary>
        public DriverOptions DriverOptions { get; }
    }
}
