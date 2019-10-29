using System;

namespace Testing.Automation.Reporter.Model
{
    public class ExceptionInfo
    {
        public ExceptionInfo()
        {
        }

        public ExceptionInfo(Exception ex)
        {
            ExceptionType = ex.GetType().FullName;
            HelpLink = ex.HelpLink;
            InnerException = ex.InnerException.ToExceptionInfo();  
            Message = ex.Message;
            Source = ex.Source;
            StackTrace = ex.StackTrace;
        }

        public string MessageHtml
        {
            get { return Markdown.ToHtml(Message); }
        }

        public string ExceptionType { get; set; }
        public string HelpLink { get; set; }
        public ExceptionInfo InnerException { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public string StackTrace { get; set; }
    }
}
