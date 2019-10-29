using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using Testing.Automation.Web.Extensions;
using Testing.Automation.Web.Interfaces;

namespace Testing.Automation.Web.Component
{
    public class Operations : Element, IOperations
    {
        public Operations(IBlock parent, Testing.Automation.Web.Extensions.By by) : base(parent, by)
        {
            OptionsEvaluator = DefaultEvaluator;
        }

        public Operations(IBlock parent, IWebElement element) : base(parent, element)
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

        public Func<IWebElement, IOption> OptionsEvaluator;
    }

    public class Operations<TResult> : Operations, IOperations<TResult>
        where TResult : IBlock
    {
        public Operations(IBlock parent, Testing.Automation.Web.Extensions.By by) : base(parent, by)
        {
            OptionsEvaluator = GenericEvaluator;
        }

        public Operations(IBlock parent, IWebElement element) : base(parent, element)
        {
            OptionsEvaluator = GenericEvaluator;
        }

        protected Func<IWebElement, IOption<TResult>> GenericEvaluator
        {
            get
            {
                return (e) => new Option<TResult>(ParentBlock, e);
            }
        }

        public virtual IEnumerable<IOption<TResult>> GetSixthColumn
        {
            get
            {

                return Tag.GetElements(Testing.Automation.Web.Extensions.By.XPath($"//*[@class=\"x-grid-item-container\"]/table/tbody/tr/td[6]")).Select(GenericEvaluator);
            }
        }

        public virtual IEnumerable<IOption<TResult>> GetFifthColumn
        {
            get
            {

                return Tag.GetElements(Testing.Automation.Web.Extensions.By.XPath($"//*[@class=\"x-grid-item-container\"]/table/tbody/tr/td[5]")).Select(GenericEvaluator);
            }
        }

        public virtual IEnumerable<IOption<TResult>> GetFourthColumn
        {
            get
            {

                return Tag.GetElements(Testing.Automation.Web.Extensions.By.XPath($"//*[@class=\"x-grid-item-container\"]/table/tbody/tr/td[4]")).Select(GenericEvaluator);
            }
        }

        public virtual IEnumerable<IOption<TResult>> GetThirthColumn
        {
            get
            {

                return Tag.GetElements(Testing.Automation.Web.Extensions.By.XPath($"//*[@class=\"x-grid-item-container\"]/table/tbody/tr/td[3]")).Select(GenericEvaluator);
            }
        }

        public virtual IEnumerable<IOption<TResult>> GetSecondColumn
        {
            get
            {

                return Tag.GetElements(Testing.Automation.Web.Extensions.By.XPath($"//*[@class=\"x-grid-item-container\"]/table/tbody/tr/td[2]")).Select(GenericEvaluator);
            }
        }

        public virtual IEnumerable<IOption<TResult>> GetFirsthColumn
        {
            get
            {

                return Tag.GetElements(Testing.Automation.Web.Extensions.By.XPath($"//*[@class=\"x-grid-item-container\"]/table/tbody/tr/td[1]")).Select(GenericEvaluator);
            }
        }
    }
}
