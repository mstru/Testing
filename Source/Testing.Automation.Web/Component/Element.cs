using Testing.Automation.Web.Interfaces;
using Testing.Automation.Web.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Testing.Automation.Web.Component
{
    public abstract class Element : SpecificBlock
    {
        protected Element(IBlock parent, Testing.Automation.Web.Extensions.By by)
            : base(parent.Session, parent.Tag.GetElement(by))
        {
            ParentBlock = parent;
        }

        protected WebDriverWait Wait { get; private set; }

        protected Element(IBlock parent, OpenQA.Selenium.By by)
            : base(parent.Session, parent.Tag.FindElement(by))
        {
            ParentBlock = parent;
        }

        protected Element(IBlock parent, IWebElement tag) 
            : base(parent.Session, tag)
        {
            ParentBlock = parent;
        }



        public IBlock ParentBlock { get; private set; }

        public virtual string Text => Tag.Text;

        public virtual bool Selected => Tag.Selected;
    }
}