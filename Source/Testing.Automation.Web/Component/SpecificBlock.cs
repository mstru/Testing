using Testing.Automation.Web.Interfaces;
using Testing.Automation.Web.Setup;
using OpenQA.Selenium;

namespace Testing.Automation.Web.Component
{
    public abstract class SpecificBlock : Block, ISpecificBlock
    {
        protected SpecificBlock(Session session, IWebElement tag) : base(session)
        {
            Tag = tag;
        }
    }
}