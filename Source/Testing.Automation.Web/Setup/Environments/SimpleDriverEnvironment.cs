﻿using Testing.Automation.Web.Setup;
using OpenQA.Selenium;
using System;

namespace Testing.Automation.Web.Setup.Environments
{
    public abstract class SimpleDriverEnvironment<TWebDriver> : IDriverEnvironment
            where TWebDriver : IWebDriver, new()
    {
        protected SimpleDriverEnvironment() : this(TimeSpan.FromSeconds(5))
        {
        }

        protected SimpleDriverEnvironment(TimeSpan timeToWait)
        {
            TimeToWait = timeToWait;
            DriverFactory = () => new TWebDriver();
        }

        protected TimeSpan TimeToWait { get; }

        public virtual IWebDriver CreateWebDriver()
        {
            var driver = DriverFactory();

            InitializeDriver(driver);
            return driver;
        }

        public virtual IWebDriver CreateWebDriver(string proxy)
        {
            return CreateWebDriver();
        }

        protected Func<IWebDriver> DriverFactory { get; set; }

        protected virtual IWebDriver InitializeDriver(IWebDriver driver)
        {
            driver.Manage().Window.Maximize();
            return driver;
        }
    }
}
