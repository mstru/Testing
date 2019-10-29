using System;
using System.Collections.Generic;
using Testing.Automation.Web.Interfaces;
using Testing.Automation.Web.Setup;
using System.Diagnostics;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using Testing.Automation.Web.Extensions;
using Testing.Automation.Common;

namespace Testing.Automation.Web.Component
{
    public class Block : IBlock
    {
        protected Block(Session session)
        {
            Session = session;
            Session.Monkey?.Invoke(this);
        }

        public Session Session { get; private set; }

        public OpenQA.Selenium.IWebElement Tag { get; protected set; }

        public virtual IPerformsDragAndDrop GetDragAndDropPerformer()
        {
            return new WebDragAndDropPerformer(Session.Driver);
        }

        public virtual void VerifyMonkeyState()
        {
        }

        public IList<OpenQA.Selenium.IWebElement> FindElements(OpenQA.Selenium.By by)
        {
            if (Tag == null)
            {
                throw new NullReferenceException("You can't call GetElements on a block without first initializing Tag.");
            }

            return Tag.FindElements(by);
        }

        public OpenQA.Selenium.IWebElement FindElement(OpenQA.Selenium.By by)
        {
            if (Tag == null)
            {
                throw new NullReferenceException("You can't call GetElement on a block without first initializing Tag.");
            }

            return Tag.FindElement(by);
        }

        public bool IsDisplayed(bool throwWhenNotDisplayed = false)
        {
            throw new System.NotImplementedException();
        }

        private bool IsLoadingDisplayed()
        {
            var result = 0;

            try
            {
                Session.Driver.SwitchTo().DefaultContent();
                Session.Driver.SwitchTo().Frame("cphShellContent_pluginContent_IFrame");
            }
            catch (Exception) { }

            try
            {
                result = Session.Driver.GetElements(Testing.Automation.Web.Extensions.By.CssClass("x-mask-loading")).Count;
            }
            catch (Exception) { }

            if (result == 0) { return true; }

            return false;
        }

        private static string _GetWaitForPageJavascript(string pageloadComplete) //bool true send True to the server and JS is case sensitive. Hence, using string and passing "true" and "false"
        {
            string javascript = "var test = function()" +
                                      "{" +
                                      "var result = false;" +
                                      "try" +
                                      "{" +
                                $"result = (top.document.getElementsByTagName('body')[0].getAttribute(\"Načítavanie...\") == \"{pageloadComplete}\")" +
                                      "}" +
                                      "catch(ex)" +
                                      "{" +
                                      "}" +
                                      "return result;" +
                                      "}; return test();";
            return javascript;
        }

        private static string _GetWaitForInnerPageJavascript(string inner, string loadComplete) //loadComplete value should either be "true" or "false"
        {
            string javascript = "var test = function()" +
                "{" +
                    "var result = false;" +
                    "try" +
                    "{" +
                                $"result = (top.frames[\"{inner}\"].contentDocument.getElementsByTagName('body')[0].getAttribute(\"Načítavanie...\") == \"{loadComplete}\")" +
                    "}" +
                    "catch(ex)" +
                    "{" +
                    "}" +
                    "return result;" +
                "}; return test();";

            return javascript;
        }

        public void RefreshPage()
        {
            ((OpenQA.Selenium.IJavaScriptExecutor)Session.Driver).ExecuteScript("top.cphShellContent_pluginContent_IFrame.location.reload();");
        }

        /// <summary>
        /// Čaká na chvíľu, kým sa prvok nenájde.
        /// </summary>
        /// <param name="webDriver">Web driver.</param>
        /// <param name="locator">Lokátor.</param>
        /// <param name="timeout">Timeout.</param>
        public void WaitUntilElementIsNoFound(By locator, double timeout)
        { 
            var wait = new WebDriverWait(Session.Driver, TimeSpan.FromSeconds(timeout));
            wait.IgnoreExceptionTypes(typeof(OpenQA.Selenium.StaleElementReferenceException), typeof(OpenQA.Selenium.NoSuchElementException));
            wait.Until(driver => Session.Driver.GetElements(locator).Count == 0);
            Thread.Sleep(2000);
        }

        /// <summary>
        /// Čaká na chvíľu, kým sa prvok nájde.
        /// </summary>
        /// <param name="webDriver">Web driver.</param>
        /// <param name="locator">Lokátor.</param>
        /// <param name="timeout">Timeout.</param>
        public void WaitUntilElementIsFound(By locator, double timeout)
        {
            var wait = new WebDriverWait(Session.Driver, TimeSpan.FromSeconds(timeout));
            wait.IgnoreExceptionTypes(typeof(OpenQA.Selenium.StaleElementReferenceException), typeof(OpenQA.Selenium.NoSuchElementException));
            wait.Until(driver => Session.Driver.GetElements(locator).Count >= 1);
        }

        private bool WaitForPageFullyLoaded(int timeout, out long timeTaken, int pollingInterval = 1000, string innerPageElement = null)
        {           
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var pageLoaded = IsLoadingDisplayed();
            while (pageLoaded == false && stopwatch.Elapsed.TotalSeconds <= timeout)
            {
                Thread.Sleep(pollingInterval);
                pageLoaded = IsLoadingDisplayed();
            }
            stopwatch.Stop();
            timeTaken = stopwatch.ElapsedMilliseconds;

            return pageLoaded;
        }

        private bool WaitForBrowserToReportReady(out long timetaken, int timeout = 10000, int pollingInterval = 250, string innerPageContainerId = null)
        {
            return WaitForPageFullyLoaded(timeout, out timetaken, pollingInterval, innerPageContainerId);
        }

        public bool WaitForPageToBeFullyLoaded(bool performOnPageCheck = false, int timeout = 0, int pollingInterval = 250, string innerPageContainerId = null)
        {
            long timetaken;

            if (timeout == 0)
            {
                timeout = BaseConfiguration.LongTimeout;
            }

            var pageFullyLoaded = WaitForBrowserToReportReady(out timetaken, BaseConfiguration.LongTimeout, pollingInterval, innerPageContainerId);

            // Ak je stránka úplne načítaná a vstupný kód požiada o hlbšiu kontrolu
            if (pageFullyLoaded && performOnPageCheck)
            {
                // Nastavenie príznaku na hodnotu metódy IsDisplayed - v tejto chvíli už môže byť asi
                pageFullyLoaded = IsLoadingDisplayed();

                if (!pageFullyLoaded)
                {
                    // Určiť koľko času zostáva, s ohľadom na uplynutý čas a povolenú hodnotu zadaného časového limitu
                    long remainingTime = timeout - timetaken;

                    // Start novej inštancie stopiek 
                    var stopwatch = new Stopwatch();
                    stopwatch.Start();

                    // Čakanie na plné načítanie stránky
                    while (pageFullyLoaded == false && stopwatch.Elapsed.TotalSeconds <= remainingTime)
                    {
                        Thread.Sleep(pollingInterval);

                        // Znovu kontrola stránky
                        pageFullyLoaded = IsLoadingDisplayed();
                    }

                    // Stop stopiek
                    stopwatch.Stop();

                    // Vypočítaj celkový čas
                    timetaken += stopwatch.ElapsedMilliseconds;
                }
            }
            return pageFullyLoaded;
        }        
    }
}