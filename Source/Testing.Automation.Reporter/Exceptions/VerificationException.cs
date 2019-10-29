using System.Collections.ObjectModel;

namespace Testing.Automation.Reporter.Exceptions
{
    public class VerificationException
        : System.Exception
    {

        public static Collection<ErrorDetail> VerifyMessages = new Collection<ErrorDetail>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public VerificationException(string message) : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public VerificationException(string message, System.Exception innerException) : base(message, innerException)
        {
            VerifyMessages.Add(new ErrorDetail(System.DateTime.Now, innerException, message));
        }
    }
}
    
