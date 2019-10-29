using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Testing.Automation.Web.Setup
{
    public interface IDriverEnvironment
    {
        IWebDriver CreateWebDriver();

        IWebDriver CreateWebDriver(string proxy);
    }
}