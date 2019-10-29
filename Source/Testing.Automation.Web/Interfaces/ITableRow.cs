using System.Collections.Generic;

namespace Testing.Automation.Web.Interfaces
{
    public interface ITableRow : IEnumerable<string>
    {
        string this[int index] { get; }
        string this[string column] { get; }
    }
}