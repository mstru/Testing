using Testing.Automation.Web.Interfaces;
using OpenQA.Selenium;
using System.Linq;
using System.Collections.Generic;
using System;


namespace Testing.Automation.Web.Component
{
    public abstract class Checkbox : Element, ICheckbox
    {
        public Checkbox(IBlock parent, Testing.Automation.Web.Extensions.By by) : base(parent, by)
        {
            OptionsEvaluator = DefaultEvaluator;
        }

        public Checkbox(IBlock parent, IWebElement tag) : base(parent, tag)
        {
            OptionsEvaluator = DefaultEvaluator;
        }

        protected Func<IWebElement, IOption> DefaultEvaluator
        {
            get
            {
                return (e) => new Option(this, e);

            }
        }

        public TCustomResult Click<TCustomResult>() where TCustomResult : IBlock
        {
            if (!Selected) Tag.Click();
            return Session.CurrentBlock<TCustomResult>(ParentBlock.Tag);
        }

        public virtual IEnumerable<IOption> GetOptions()
        {
            return Session.Driver.FindElements(By.CssSelector(".x-boundlist-list-ct :not([style*='right: auto']) .x-boundlist-item")).Select(OptionsEvaluator);
        }

        public Func<IWebElement, IOption> OptionsEvaluator;
    }

    public class Checkbox<TResult> : Checkbox, ICheckbox<TResult> 
        where TResult : IBlock
    {
        public virtual TResult Click()
        {
            Tag.Click();
            return Session.CurrentBlock<TResult>(ParentBlock.Tag);
        }

        public virtual TResult Check()
        {
            if (!Selected) Tag.Click();
            OptionsEvaluator = GenericEvaluator;
            return Session.CurrentBlock<TResult>(ParentBlock.Tag);
        }

        public Checkbox(IBlock parent, Testing.Automation.Web.Extensions.By by) : base(parent, by)
        {
            Tag.Click();
            OptionsEvaluator = GenericEvaluator;
            
        }

        public Checkbox(IBlock parent, IWebElement element) : base(parent, element)
        {            
            Tag.Click();
            OptionsEvaluator = GenericEvaluator;
        }

        protected Func<IWebElement, IOption<TResult>> GenericEvaluator
        {
            get
            {
                return (e) => new Option<TResult>(ParentBlock, e);
            }
        }

        public virtual IEnumerable<IOption<TResult>> Options
        {
            get { return Session.Driver.FindElements(By.CssSelector(".x-boundlist-list-ct :not([style*='right: auto']) .x-boundlist-item")).Select(GenericEvaluator); }
        }
    }
}