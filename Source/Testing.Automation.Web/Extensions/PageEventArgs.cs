namespace Testing.Automation.Web.Extensions
{
    public class PageEventArgs
    {
        public PageEventArgs(string url)
        {
            this.Url = url;
        }

        public string Url { get; private set; }
    }
}
