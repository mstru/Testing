namespace Testing.Automation.Web.Interfaces
{
    public interface ITableBar
    {

        /// <summary>
        /// Kliknutie na zalozku podla indexu poradia.
        /// </summary>
        /// <param name="tabIndex">0,1,2,3,4....</param>
        ITableBar Page(int tabIndex);

        /// <summary>
        /// Kliknutie na zalozku podla jej pomenovania.
        /// </summary>
        /// <param name="tabName"></param>
        ITableBar Page(string tabName);
    }
}
