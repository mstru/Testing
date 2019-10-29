using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Testing.Automation.Web.Extensions;
using Testing.Automation.Web.Interfaces;

namespace Testing.Automation.Web.Component
{
    public class SubjectsFilter : Element, ISubjectsFilter
    {
        public SubjectsFilter(IBlock parent, Testing.Automation.Web.Extensions.By by) : base(parent, by)
        {
            OptionsEvaluator = DefaultEvaluator;
        }

        public SubjectsFilter(IBlock parent, IWebElement element) : base(parent, element)
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

    public class SubjectsFilter<TResult> : SubjectsFilter, ISubjectsFilter<TResult>
        where TResult: IBlock
    {
        public SubjectsFilter(IBlock parent, Testing.Automation.Web.Extensions.By by) : base(parent, by)
        {
            Tag.Click();
            Thread.Sleep(3000);
            OptionsEvaluator = GenericEvaluator;
        }

        public SubjectsFilter(IBlock parent, IWebElement element) : base(parent, element)
        {
            Tag.Click();
            Thread.Sleep(3000);
            OptionsEvaluator = GenericEvaluator;
        }

        protected Func<IWebElement, IOption<TResult>> GenericEvaluator
        {
            get
            {
                return (e) => new Option<TResult>(ParentBlock, e);
            }
        }
        
        public virtual IEnumerable<IOption<TResult>> GetSubjectFilter
        {
            get
            {
                                                                                                        
                return Tag.GetElements(Testing.Automation.Web.Extensions.By.XPath("//div[@id='cbSubjectFilter_pickerWindow_gridParticipants-body']/div/div/table/tbody/tr/td[3]")).Select(GenericEvaluator);
            }
        }

        public virtual IEnumerable<IOption<TResult>> GetSubjectEicFilter
        {
            get
            {                                                                                                       
                return Tag.GetElements(Testing.Automation.Web.Extensions.By.XPath("//div[@id='cbSubjectFilter_pickerWindow_gridParticipants-body']/div/div/table/tbody/tr/td[4]")).Select(GenericEvaluator);
            }
        }

        public virtual IEnumerable<IOption<TResult>> GetSubjects
        {
            get
            {
                return Tag.GetElements(Testing.Automation.Web.Extensions.By.XPath("//div[@id='cbSubjects_pickerWindow_gridParticipants-body']/div/div/table/tbody/tr/td[3]")).Select(GenericEvaluator);
            }
        }

        public virtual IEnumerable<IOption<TResult>> GetSubject
        {
            get
            {                                                              
                return Tag.GetElements(Testing.Automation.Web.Extensions.By.XPath("//div[@id='cbSubject_pickerWindow_gridParticipants-body']/div/div/table/tbody/tr/td[3]")).Select(GenericEvaluator);
            }
        }

        public virtual IEnumerable<IOption<TResult>> GetSubjectEic
        {
            get
            {
                return Tag.GetElements(Testing.Automation.Web.Extensions.By.XPath("//div[@id='cbSubject_pickerWindow_gridParticipants-body']/div/div/table/tbody/tr/td[4]")).Select(GenericEvaluator);
            }
        }
    }
}
