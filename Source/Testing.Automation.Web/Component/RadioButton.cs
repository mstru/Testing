using Testing.Automation.Web.Interfaces;
using OpenQA.Selenium;
using Testing.Automation.Web.Extensions;

namespace Testing.Automation.Web.Component
{
    public class RadioButton<TResult> : Option<TResult> where TResult : IBlock
    {
        public RadioButton(IBlock parent, Testing.Automation.Web.Extensions.By by) : base(parent, by)
        {
        }

        public RadioButton(IBlock parent, IWebElement element) : base(parent, element)
        {
        }

        public override string Text
        {
            get { return ParentBlock.Tag.FindElement(OpenQA.Selenium.By.CssSelector("label[for=\"" + Tag.GetId() + "\"]")).Text; }
        }
    }
}