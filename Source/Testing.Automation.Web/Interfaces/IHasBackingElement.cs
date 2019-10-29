using OpenQA.Selenium;

namespace Testing.Automation.Web.Interfaces
{
    public interface IHasBackingElement
    {
        IWebElement Tag { get; }
    }
}