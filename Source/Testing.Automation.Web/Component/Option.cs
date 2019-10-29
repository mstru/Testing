using Testing.Automation.Web.Interfaces;
using OpenQA.Selenium;

namespace Testing.Automation.Web.Component
{
    public class Option : Element, IOption
    {
        public Option(IBlock parent, Testing.Automation.Web.Extensions.By by) : base(parent, by)
        {
        }

        public Option(IBlock parent, IWebElement element) : base(parent, element)
        {
        }

        public virtual TResult Click<TResult>() where TResult : IBlock
        {
            ParentBlock.Tag.Click();
            Tag.Click();
            return Session.CurrentBlock<TResult>(ParentBlock.Tag);
        }

        public virtual TResult ClickJs<TResult>() where TResult : IBlock
        {
            ParentBlock.Tag.Click();
            Tag.Click();
            return Session.CurrentBlock<TResult>(ParentBlock.Tag);
        }

        public virtual TResult ClickWait<TResult>() where TResult : IBlock
        {
            return Session.CurrentBlock<TResult>(ParentBlock.Tag);
        }

    }
        

    public class Option<TResult> : Clickable<TResult>, IOption<TResult> where TResult : IBlock
    {
        public Option(IBlock parent, Testing.Automation.Web.Extensions.By by) : base(parent, by)
        {
        }

        public Option(IBlock parent, IWebElement element) : base(parent, element)
        {
        }
    }
}