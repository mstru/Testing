using Testing.Automation.Web.Interfaces;
using Testing.Automation.Common.DateTimes.Extensions;
using OpenQA.Selenium;
using System.Globalization;
using Testing.Automation.Common;

namespace Testing.Automation.Web.Component
{
    public class DateField : TextField, IDateField
    {
        public DateField(IBlock parent, Testing.Automation.Web.Extensions.By by) : base(parent, by)
        {
        }

        public DateField(IBlock parent, IWebElement tag) : base(parent, tag)
        {
        }

        public virtual TCustomResult EnterDate<TCustomResult>(System.DateTime date) where TCustomResult : IBlock
        {
            var executor = (IJavaScriptExecutor)Session.Driver;
            string myDate = date.ToString("d", new CultureInfo(BaseConfiguration.Culture));
            executor.ExecuteScript(string.Format("arguments[0].value = '{0:d}';", myDate).Replace(" ", ""), Tag);

            return Session.CurrentBlock<TCustomResult>(ParentBlock.Tag);
        }

        public virtual TCustomResult EnterYear<TCustomResult>(System.DateTime date) where TCustomResult : IBlock
        {
            var executor = (IJavaScriptExecutor)Session.Driver;
            executor.ExecuteScript(string.Format("arguments[0].value = '{0:yyyy}';", date), Tag);

            return Session.CurrentBlock<TCustomResult>(ParentBlock.Tag);
        }



        public virtual System.DateTime? Value
        {
            get
            {
                System.DateTime result;
                return System.DateTime.TryParse(Text ?? string.Empty, out result) ? result : new System.DateTime?();
            }
        }
    }

    public class DateField<TResult> : DateField, IDateField<TResult>
        where TResult : IBlock
    {
        public DateField(IBlock parent, Testing.Automation.Web.Extensions.By by) : base(parent, by)
        {
        }

        public DateField(IBlock parent, IWebElement element) : base(parent, element)
        {
        }

        public virtual TResult EnterDate(System.DateTime date)
        {
            return EnterDate<TResult>(date);
        }

        public TResult Press(Key key)
        {
            return Press<TResult>(key);
        }

        public virtual TResult EnterText(string text)
        {
            return EnterText<TResult>(text);
        }

        public virtual TResult EnterText(double text)
        {
            return EnterText<TResult>(text);
        }

        public virtual TResult SelectedText(string text)
        {
            return SelectedText<TResult>(text);
        }

        public virtual TResult AppendText(string text)
        {
            return AppendText<TResult>(text);
        }

        public virtual TResult EnterDateOffset(System.DateTime DateToday, int offset)
        {
            return EnterDate<TResult>(DateTimeExtensions.NextDay(DateToday, offset));
        }

        public virtual TResult EnterPreviousYearOffset(System.DateTime DateToday, int offset)
        {
            return EnterYear<TResult>(DateTimeExtensions.PreviousYear(DateToday, offset));
        }

        public virtual TResult EnterNextYearOffset(System.DateTime DateToday, int offset)
        {
            return EnterYear<TResult>(DateTimeExtensions.NextYear(DateToday, offset));
        }

        public virtual TResult EnterDateFirstDayOfPreviousQuarter(System.DateTime previousDate)
        {
            return EnterDate<TResult>(DateTimeExtensions.FirstDayOfPreviousQuarter(previousDate));
        }

        public virtual TResult EnterDateLastDayOfYear(System.DateTime currentDate)
        {
            return EnterDate<TResult>(DateTimeExtensions.LastDayOfYear(currentDate));
        }

        public virtual TResult EnterDateFirstDayOfYear(System.DateTime currentDate)
        {
            return EnterDate<TResult>(DateTimeExtensions.FirstDayOfYear(currentDate));
        }

        public virtual TResult EnterDateToday(System.DateTime currentDate)
        {
            return EnterDate<TResult>(DateTimeExtensions.CurrentDay(currentDate));
        }
    }
}