namespace Testing.Automation.Web.Extensions.Interface
{
    public interface IDriver :
        IElementFinder,
        IBrowser,
        IJavaScriptInvoker,
        INavigationService
    {
    }
}
