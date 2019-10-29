using Testing.Automation.Reporter.Model;

namespace Testing.Automation.Reporter.EventArgs
{
    public class FeatureEventArgs : ReportEventArgs
    {
        public FeatureEventArgs(ReporterManager reporter)
            : base(reporter)
        {
            Feature = Reporter.CurrentFeature;
        }

        public Feature Feature { get; internal set; }
    }
}
