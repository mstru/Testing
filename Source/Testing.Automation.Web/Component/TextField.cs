using Testing.Automation.Web.Interfaces;
using OpenQA.Selenium;
using System;
using System.Threading;
using Testing.Automation.Common;

namespace Testing.Automation.Web.Component
{
    public class TextField : Element, ITextField
    {
        public TextField(IBlock parent, Testing.Automation.Web.Extensions.By by) : base(parent, by)
        {
        }

        public TextField(IBlock parent, OpenQA.Selenium.By by) : base(parent, by)
        {
        }

        public TextField(IBlock parent, IWebElement tag) : base(parent, tag)
        {
        }

        public TResult Press<TResult>(Key key) where TResult : IBlock
        {
            try
            {
                Tag.SendKeys(key.Value);
            }
            catch (InvalidOperationException)
            {
                const string js = "arguments[0].scrollIntoView(true);";
                ((IJavaScriptExecutor)Session.Driver).ExecuteScript(js, Tag);
                Thread.Sleep(1000); //Neviem prečo, ale je potrebný tento riadko ak sa pracuje z js
                Tag.SendKeys(key.Value);
            }

            return Session.CurrentBlock<TResult>(ParentBlock.Tag);
        }

        public virtual TCustomResult EnterText<TCustomResult>(string text) where TCustomResult : IBlock
        {               
            try
            {
                Tag.Clear();
            }
            catch (InvalidOperationException)
            {
                const string js = "arguments[0].scrollIntoView(true);";
                ((IJavaScriptExecutor)Session.Driver).ExecuteScript(js, Tag);
                Thread.Sleep(1000); //Neviem prečo, ale je potrebný tento riadko ak sa pracuje z js
                Tag.Clear();
            }

            return AppendText<TCustomResult>(text);
        }

        public virtual TCustomResult EnterText<TCustomResult>(double text) where TCustomResult : IBlock
        {
            try
            {
                Tag.Clear();
            }
            catch (InvalidOperationException)
            {
                const string js = "arguments[0].scrollIntoView(true);";
                ((IJavaScriptExecutor)Session.Driver).ExecuteScript(js, Tag);
                Thread.Sleep(1000); //Neviem prečo, ale je potrebný tento riadko ak sa pracuje z js
                Tag.Clear();
            }

            return AppendText<TCustomResult>(text);
        }

        public virtual TCustomResult SelectText<TCustomResult>(string text) where TCustomResult : IBlock
        {
            return SelectedText<TCustomResult>(text);
        }


        public virtual TResult AppendText<TResult>(string text) where TResult : IBlock
        {
            Tag.SendKeys(EmptyIfNull(text));

            return Session.CurrentBlock<TResult>(ParentBlock.Tag);
        }

        public virtual TResult AppendText<TResult>(double text) where TResult : IBlock
        {
            string myText = (BaseConfiguration.Culture == "En") ? text.ToString().Replace(",", ".") : text.ToString().Replace(".", ",");
            Tag.SendKeys(myText);

            return Session.CurrentBlock<TResult>(ParentBlock.Tag);
        }

        public virtual TResult SelectedText<TResult>(string text) where TResult : IBlock
        {
            var select =
                new OpenQA.Selenium.Support.UI.SelectElement(Tag);
            select.SelectByText(text);
            return Session.CurrentBlock<TResult>(ParentBlock.Tag);
        }

        public string EmptyIfNull(string text)
        {
            return text?.ToString() ?? string.Empty;
        }

        public override string Text => Tag.GetAttribute("value");
    }

    public class TextField<TResult> : TextField, ITextField<TResult> where TResult : IBlock
    {
        public TextField(IBlock parent, Testing.Automation.Web.Extensions.By by) : base(parent, by)
        {
        }

        public TextField(IBlock parent, OpenQA.Selenium.By by) : base(parent, by)
        {
        }

        public TextField(IBlock parent, IWebElement element) : base(parent, element)
        {
        }

        public TResult Press(Key key)
        {
            return Press<TResult>(key);
        }

        public virtual TResult EnterText(string text)
        {
            return EnterText<TResult>(text);
        }

        public virtual TResult EnterText(double text)
        {
            return EnterText<TResult>(text);
        }

        public virtual TResult SelectedText(string text)
        {
            return SelectedText<TResult>(text);
        }

        public virtual TResult AppendText(string text)
        {
            return AppendText<TResult>(text);
        }
    }
}