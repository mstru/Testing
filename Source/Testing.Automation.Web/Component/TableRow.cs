using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Testing.Automation.Web.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;


namespace Testing.Automation.Web.Component
{
    public class TableRow : Element, ITableRow
    {
        int col = 0;

        private readonly IDictionary<string, string> _data;

        public TableRow(IBlock parent, Testing.Automation.Web.Extensions.By @by) : base(parent, @by)
        {
            _data = ParentBlock.Tag
                .FindElement(By.TagName("tr"))
                .FindElements(By.TagName("td"))
                .Zip(FindElements(By.TagName("td")),
                    (header, cell) => new KeyValuePair<string, string>(header.Text, cell.Text))
                .Where(x => !string.IsNullOrWhiteSpace(x.Key))
                .ToDictionary(x => $"Col{col += 1}", x => x.Value);
        }

        public TableRow(IBlock parent, IWebElement tag) : base(parent, tag)
        {
            _data = ParentBlock.Tag
                .FindElement(By.TagName("tr"))
                .FindElements(By.TagName("td"))
                .Zip(FindElements(By.TagName("td")),
                    (header, cell) => new KeyValuePair<string, string>(header.Text, cell.Text))
                .Where(x => !string.IsNullOrWhiteSpace(x.Key))
                .ToDictionary(x => $"Col{col += 1}", x => x.Value);
        }

        public string this[int index] => _data.Values.ElementAt(index);

        public string this[string column] => _data[column];

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<string> GetEnumerator()
        {
            return _data.Values.GetEnumerator();
        }
    }
}