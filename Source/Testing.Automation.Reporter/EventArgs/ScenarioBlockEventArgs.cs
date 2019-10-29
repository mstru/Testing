using Testing.Automation.Reporter.Model;

namespace Testing.Automation.Reporter.EventArgs
{
    public class ScenarioBlockEventArgs : ScenarioEventArgs
    {
        public ScenarioBlockEventArgs(ReporterManager reporter)
            : base(reporter)
        {
            ScenarioBlock = reporter.CurrentScenarioBlock;
        }

        public ScenarioBlock ScenarioBlock { get; internal set; }
    }
}
