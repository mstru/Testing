using AventStack.ExtentReports;
using System;

namespace Testing.Automation.Reporter.Model
{
    public abstract class ReportItem
    {
        public ExtentTest Node { get; set; }
        public string StepInfo { get; set; }
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        //public object UserData { get; set; }
        //public object GeneratorData { get; set; }
        public virtual TestResult Result { get; set; }
    }
}
