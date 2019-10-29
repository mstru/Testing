﻿using OpenQA.Selenium.Support.UI;
using System;
using Testing.Automation.Web.Extensions;
using Testing.Automation.Web.Setup;

namespace Testing.Automation.Web.Component
{
    /// <summary>
    ///     Base block for typical web pages allowing for specifying a common wait timeout for finding elements.
    /// </summary>
    public abstract class WebBlock : Block
    {

        /// <summary>
        ///     Constructor that allows for overriding the default timeout for waits.
        /// </summary>
        /// <param name="session">The session to be used for finding elements in the derived page.</param>
        /// <param name="timeout">The timeout period for waits represented as a TimeSpan</param>
        protected WebBlock(Session session, TimeSpan timeout) : base(session)
        {
            this.Pause(200);
            WaitForPageToBeFullyLoaded(true);
            Wait = new WebDriverWait(Session.Driver, timeout);
            Tag = Wait.Until(driver => driver.FindElement(OpenQA.Selenium.By.TagName("body")));            
        }

        /// <summary>
        ///     Default constructor.
        /// </summary>
        /// <remarks>
        ///     The default timeout for waiting for elements is 3000 ticks (3-100 nano seconds).  If you need to override this
        ///     value, call the other constructor.
        /// </remarks>
        /// <param name="session">The sessionto be used for finding elements in the derived page.</param>
        protected WebBlock(Session session) : this(session, TimeSpan.FromSeconds(15))
        {
        }

        /// <summary>
        ///     A common wait timeout that can be used when trying to find elements within derived pages or blocks.
        /// </summary>
        protected WebDriverWait Wait { get; private set; }
    }
}