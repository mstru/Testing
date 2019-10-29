using System;
using static Testing.Automation.Web.Extensions.By;

namespace Testing.Automation.Web.Extensions
{
    public static class ByExtensions
    {
        public static OpenQA.Selenium.By ToSeleniumBy(this By by)
        {
            switch (by.Type)
            {                
                case SearchTypes.Id:
                    return OpenQA.Selenium.By.Id(by.Value);

                case SearchTypes.Tag:
                    return OpenQA.Selenium.By.TagName(by.Value);

                case SearchTypes.CssClass:
                    return OpenQA.Selenium.By.ClassName(by.Value);

                case SearchTypes.XPath:
                    return OpenQA.Selenium.By.XPath(by.Value);

                case SearchTypes.CssSelector:
                    return OpenQA.Selenium.By.CssSelector(by.Value);

                case SearchTypes.Name:
                    return OpenQA.Selenium.By.Name(by.Value);

                default:
                    throw new Exception(string.Format("The specified locator type was not find: {0}", by.Type));
            }
        }        
    }
}
