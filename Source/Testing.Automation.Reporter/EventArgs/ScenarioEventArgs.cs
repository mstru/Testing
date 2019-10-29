using Testing.Automation.Reporter.Model;

namespace Testing.Automation.Reporter.EventArgs
{
    public class ScenarioEventArgs : FeatureEventArgs
    {
        public ScenarioEventArgs(ReporterManager reporter)
            : base(reporter)
        {
            Scenario = Reporter.CurrentScenario;
        }

        public Scenario Scenario { get; internal set; }
    }
}
