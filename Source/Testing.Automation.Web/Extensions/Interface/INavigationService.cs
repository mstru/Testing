using System;

namespace Testing.Automation.Web.Extensions.Interface
{
    public interface INavigationService
    {
        event EventHandler<PageEventArgs> Navigated;

        string Title { get; }

        string Navigate(string modul, string page, string atribute);

        void NavigateByAbsoluteUrl(string modul, string page, bool useDecodeUrl = true);

        void NavigateByAbsoluteUrlWithChangeFrame(string modul, string page, bool useDecodeUrl = true);

        void Refresh();

        void WaitForUrl(string url);

        void WaitForPartialUrl(string url);
    }
}
