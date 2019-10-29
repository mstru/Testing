using OpenQA.Selenium;
using System.Collections.Generic;
using Testing.Automation.Web.Component;

namespace Testing.Automation.Web.Interfaces
{
    public interface ITable
    {
        IEnumerable<string> Headers { get; }
        IEnumerable<ITableRow> Row { get; }
        IEnumerable<string> Footers { get; }
        IEnumerable<IWebElement> RowsCount { get; }
        T HeaderAs<T>() where T : Element;
        IEnumerable<T> RowsAs<T>() where T : Element;
        T FooterAs<T>() where T : Element;
    }
}