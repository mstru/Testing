using System;
using System.Collections.Generic;
using System.Linq;
using Testing.Automation.Web.Interfaces;
using OpenQA.Selenium;
using System.Threading;

namespace Testing.Automation.Web.Component
{
    public class Table : Element, ITable
    {
        public Table(IBlock parent, Testing.Automation.Web.Extensions.By @by) : base(parent, @by)
        {
            WaitForPageToBeFullyLoaded(true);
        }

        public Table(IBlock parent, IWebElement tag) : base(parent, tag)
        {
            WaitForPageToBeFullyLoaded(true);
        }

        public IEnumerable<string> Headers
        {
            get
            {
                Thread.Sleep(2000); //mozno pomoze kvoli -  The element reference of  is stale; either the element is no longer attached to the DOM, it is not in the current frame context, or the document has been refreshed
                return FindElement(By.TagName("thead"))
                    .FindElement(By.TagName("tr"))
                    .FindElements(By.TagName("th"))
                        .Select(x => x.Text);
            }
        }

        public IEnumerable<ITableRow> Row
        {
            get
            {
                Thread.Sleep(2000); //mozno pomoze kvoli -  The element reference of  is stale; either the element is no longer attached to the DOM, it is not in the current frame context, or the document has been refreshed
                return FindElement(By.TagName("tbody"))
                    .FindElements(By.TagName("tr"))
                    .Select(
                          (x, i) => new TableRow(this, Tag.FindElement(OpenQA.Selenium.By.CssSelector($"tbody > tr:nth-child({i + 1})"))));
            }   
        }

        public IEnumerable<IWebElement> RowsCount
        {
            get
            {
                return FindElements(By.TagName("table"));
            }
        }

        public IEnumerable<string> Footers
        {
            get
            {
                return FindElement(By.TagName("tfoot"))
                    .FindElement(By.TagName("tr"))
                    .FindElements(By.TagName("td"))
                        .Select(x => x.Text);
            }
        }

        public T HeaderAs<T>()
            where T : Element
        {
            return Create<T>(this, By.TagName("thead"));
        }

        public IEnumerable<T> RowsAs<T>()
            where T : Element
        {
            return FindElement(By.TagName("tbody"))
                  .FindElements(By.TagName("tr"))
                    .Select((x, i) => Create<T>(this, By.CssSelector($"tbody > tr:nth-child({i + 1})")));
        }

        public T FooterAs<T>()
            where T : Element
        {
            return Create<T>(this, By.TagName("tfoot"));
        }

        protected static T Create<T>(IBlock parent, By @by)
        {
            return (T) Activator.CreateInstance(typeof (T), parent, @by);
        }

        protected static T Create<T>(IBlock parent, IWebElement tag)
        {
            return (T) Activator.CreateInstance(typeof (T), parent, tag);
        }
    }
}