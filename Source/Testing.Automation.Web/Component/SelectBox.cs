using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using Testing.Automation.Web.Interfaces;
using Testing.Automation.Web.Extensions;
using Testing.Automation.Web.Setup;


namespace Testing.Automation.Web.Component
{
    public class SelectBox : Element, ISelectBox
    {
        public SelectBox(IBlock parent, Testing.Automation.Web.Extensions.By by) : base(parent, by)
        {
            OptionsEvaluator = DefaultEvaluator;
        }

        public SelectBox(IBlock parent, IWebElement element) : base(parent, element)
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

        public virtual IEnumerable<IOption> GetOptions()
        {
            return Session.Driver.FindElements(OpenQA.Selenium.By.CssSelector(".x-boundlist-list-ct :not([style*='right: auto']) .x-boundlist-item")).Select(OptionsEvaluator);
        }

        public Func<IWebElement, IOption> OptionsEvaluator;
    }

    public class SelectBox<TResult> : SelectBox, ISelectBox<TResult>
        where TResult : IBlock
    {
        public SelectBox(IBlock parent, Testing.Automation.Web.Extensions.By by) : base(parent, by)
        {
            Tag.Click();
            Thread.Sleep(2000);
            OptionsEvaluator = GenericEvaluator;
        }

        public SelectBox(IBlock parent, IWebElement element) : base(parent, element)
        {
            Tag.Click();
            Thread.Sleep(2000);
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
            get { return Session.Driver.FindElements(OpenQA.Selenium.By.CssSelector(".x-boundlist-list-ct :not([style*='right: auto']) .x-boundlist-item")).Select(GenericEvaluator); }            
        }
    }
}