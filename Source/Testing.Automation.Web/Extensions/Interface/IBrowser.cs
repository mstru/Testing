namespace Testing.Automation.Web.Extensions.Interface
{
    public interface IBrowser
    {
        string SourceString { get; }

        void SwitchToFrame(IFrame newContainer);

        IFrame GetFrameByName(string frameName);

        void SwitchToDefault();

        void Quit();

        void WaitForAjax();

        void WaitUntilReady();

        void FullWaitUntilReady();

        void RefreshDomTree();

        void ClickBackButton();

        void ClickForwardButton();

        void LaunchNewBrowser();

        void MaximizeBrowserWindow();

        void ClickRefresh();
    }
}
