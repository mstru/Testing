using System.Collections.Generic;
using System.Linq;
using Testing.Automation.Web.Interfaces;
using OpenQA.Selenium;
using Testing.Automation.Web.Extensions;

namespace Testing.Automation.Web.Component
{
    public class RadioButtons<TResult> : IRadioButtons<TResult> where TResult : IBlock
    {

        public RadioButtons(IBlock parent, Testing.Automation.Web.Extensions.By by)
        {
            ParentBlock = parent;
            By = by;
        }

        private IBlock ParentBlock { get; set; }
        private Testing.Automation.Web.Extensions.By By { get; set; }

        public virtual IEnumerable<IOption<TResult>> Options
        {
            get
            {
                return ParentBlock.Tag.FindElements(By.ToSeleniumBy())
                    .Where(opt => opt.Displayed)
                    .Select(opt => new RadioButton<TResult>(ParentBlock, opt));
            }
        }
    }
}