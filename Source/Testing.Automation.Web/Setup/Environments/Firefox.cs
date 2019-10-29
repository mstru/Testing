using System;
using OpenQA.Selenium.Firefox;

namespace Testing.Automation.Web.Setup.Environments
{
    public class Firefox : FirefoxDriverEnvironment<FirefoxDriver>
    {
        public Firefox()
        {                      
        }

        public Firefox(TimeSpan timeToWait) 
            : base(timeToWait)
        {           
        }    
    }
}