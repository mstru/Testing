using System;
using Testing.Automation.Reporter.ReportingAspect;

namespace Testing.Automation.Reporter
{
    [Reporting]
    public abstract class ReportingStepDefinitions : ContextBoundObject
    {
        public void ReportStep(Action action, params object[] args)
        {
            Reporter.ExecuteStep(action, args);
        }
    }
}
