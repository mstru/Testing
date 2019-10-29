using System;
using OpenQA.Selenium.Chrome;
using Testing.Automation.Web.Setup.Environments;

namespace Testing.Automation.Web.Setup.Environments
{
    public class Chrome : SimpleDriverEnvironment<ChromeDriver>
    {
        public Chrome()
        {
        }

        public Chrome(TimeSpan timeToWait) : base(timeToWait)
        {
        }
    }
}