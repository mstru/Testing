using System;
using OpenQA.Selenium.IE;
using Testing.Automation.Web.Setup.Environments;

namespace Testing.Automation.Web.Setup.Environments
{
    public class InternetExplorer : SimpleDriverEnvironment<InternetExplorerDriver>
    {
        public InternetExplorer()
        {
        }

        public InternetExplorer(TimeSpan timeToWait) : base(timeToWait)
        {
        }
    }
}