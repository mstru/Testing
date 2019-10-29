using Testing.Automation.Reporter.Model;

namespace Testing.Automation.Reporter.EventArgs
{
    public class ReportEventArgs : System.EventArgs
    {
        public ReportEventArgs(ReporterManager reporter)
        {
            Reporter = reporter;
            Report = reporter.Report;
        }

        public ReporterManager Reporter { get; internal set; }
        public Report Report { get; internal set; }
    }
}
