using Testing.Automation.Web.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Threading;
using System;

namespace Testing.Automation.Web.Component
{
    public class Clickable : Element, IClickable, IDoubleClickable, IHoverAndClick
    {
        private readonly Testing.Automation.Web.Extensions.By by;

        public Clickable(IBlock parent, Testing.Automation.Web.Extensions.By by) : base(parent, by)
        {
            this.by = by;
        }

        public Clickable(IBlock parent, IWebElement element) : base(parent, element)
        {
        }

        public virtual TResult DoubleClick<TResult>()
            where TResult : IBlock
        {
            new Actions(Session.Driver)
                .MoveToElement(Tag)
                .DoubleClick()
                .Build()
                .Perform();

            return Session.CurrentBlock<TResult>(ParentBlock.Tag);
        }

        public virtual TResult HoverAndClick<TResult>(int offsetX, int offsetY)
            where TResult : IBlock
        {
            new Actions(Session.Driver)
                .MoveToElement(Tag, offsetX, offsetY)
                .Click()
                .Build()
                .Perform();
            return Session.CurrentBlock<TResult>(ParentBlock.Tag);
        }

        public virtual TResult Click<TResult>()
               where TResult : IBlock
        {
            try
            {
                const string js = "arguments[0].scrollIntoView(true);";
                ((IJavaScriptExecutor)Session.Driver).ExecuteScript(js, Tag);
                Thread.Sleep(500); //Neviem prečo, ale je potrebný tento riadko ak sa pracuje z js
                Tag.Click();
            }
            catch (InvalidOperationException)
            {
                const string js = "arguments[0].scrollIntoView(true);";
                ((IJavaScriptExecutor)Session.Driver).ExecuteScript(js, Tag);
                Thread.Sleep(1000); //Neviem prečo, ale je potrebný tento riadko ak sa pracuje z js
                Tag.Click();
            }

            return Session.CurrentBlock<TResult>(ParentBlock.Tag);
        }

        public virtual TResult ClickWait<TResult>()
            where TResult : IBlock
        {
            try
            {
                Tag.Click();
                WaitUntilElementIsNoFound(by, 20);
            }
            catch (InvalidOperationException)
            {
                const string js = "arguments[0].scrollIntoView(true);";
                ((IJavaScriptExecutor)Session.Driver).ExecuteScript(js, Tag);
                Thread.Sleep(1000); //Neviem prečo, ale je potrebný tento riadko ak sa pracuje z js
                Tag.Click();
                WaitUntilElementIsNoFound(by, 20);
            }
            return Session.CurrentBlock<TResult>(ParentBlock.Tag);
        }

        public virtual TResult ClickJs<TResult>()
            where TResult : IBlock
        {
            const string js = "arguments[0].click();";

            try
            {
                ((IJavaScriptExecutor)Session.Driver).ExecuteScript(js, Tag);
            }
            catch (InvalidOperationException)
            {
                Thread.Sleep(1000); //Neviem prečo, ale je potrebný tento riadko ak sa pracuje z js
                ((IJavaScriptExecutor)Session.Driver).ExecuteScript(js, Tag);

            }
            return Session.CurrentBlock<TResult>(ParentBlock.Tag);
        }

    }

    public class Clickable<TResult> : Clickable, IClickable<TResult>, IDoubleClickable<TResult>, IHoverAndClick<TResult>
        where TResult : IBlock
    {

        public Clickable(IBlock parent, Testing.Automation.Web.Extensions.By by) : base(parent, by)
        {
        }

        public Clickable(IBlock parent, IWebElement element) : base(parent, element)
        {
        }

        public virtual TResult Click()
        {
            return Click<TResult>();
        }

        public virtual TResult ClickJs()
        {
            return ClickJs<TResult>();
        }

        public virtual TResult HoverAndClick(int offsetX, int offsetY)
        {
            return HoverAndClick<TResult>(offsetX, offsetY);
        }

        public virtual TResult DoubleClick()
        {
            return DoubleClick<TResult>();
        }
    }
}