using Testing.Automation.Reporter.Model;

namespace Testing.Automation.Reporter.EventArgs
{
    public class StepEventArgs : ScenarioBlockEventArgs
    {
        public StepEventArgs(ReporterManager reporter)
            : base(reporter)
        {
            Step = reporter.CurrentStep;
        }

        public Step Step { get; internal set; }
    }
}
