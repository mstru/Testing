using OpenQA.Selenium;
using System.Linq;
using Testing.Automation.Web.Interfaces;

namespace Testing.Automation.Web.Component
{
    public class TableBar : Element, ITableBar
    {
        public TableBar(IBlock parent, Testing.Automation.Web.Extensions.By @by) : base(parent, @by)
        {
        }

        public TableBar(IBlock parent, IWebElement tag) : base(parent, tag)
        {
        }

        /// <summary>
        /// Kliknutie na zalozku podla indexu poradia.
        /// </summary>
        /// <param name="tabIndex">0,1,2,3,4....</param>
        public ITableBar Page(int tabIndex)
        {
            FindElements(By.TagName("a"))
               .ElementAt(tabIndex)
               .Click();
            return this;
        }

        /// <summary>
        /// Kliknutie na zalozku podla jej pomenovania.
        /// </summary>
        /// <param name="tabName"></param>
        public ITableBar Page(string tabName)
        {
            FindElement(By.XPath("//span[contains(text(), \"" + tabName + "\")]"))
                .Click();
            return this;
        }
    }
}
