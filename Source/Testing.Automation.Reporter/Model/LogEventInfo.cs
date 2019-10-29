using AventStack.ExtentReports;

namespace Testing.Automation.Reporter.Model
{
    public class LogEventInfo
    {
        public Status Status { get; internal set; }

        public string Message { get; internal set; }

        public ExceptionInfo Exception { get; internal set; }

        public CodeBlockType CodeBlockType { get; internal set; }
    }
}
