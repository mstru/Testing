
namespace Testing.Automation.Web.Interfaces
{
    /// <summary>
    ///     An input[type='date'] in a browser.
    /// </summary>
    public interface IDateField : ITextField
    {

        /// <summary>
        ///     Gets the value as a DateTime. Tries to parse according to CurrentUICulture.
        /// </summary>
        /// <value>
        ///     The value as a DateTime or null if it couldn't be parsed.
        /// </value>
        System.DateTime? Value { get; }

        /// <summary>
        ///     Enters a date into the date field
        /// </summary>
        /// <typeparam name="TCustomResult">The type of the block this element is on.</typeparam>
        /// <param name="date">The date to enter</param>
        /// <returns>The current block</returns>
        TCustomResult EnterDate<TCustomResult>(System.DateTime date) where TCustomResult : IBlock;
    }

    /// <summary>
    ///     An input[type='date'] in a browser.
    /// </summary>
    /// <typeparam name="TResult">The type of the block this element is on.</typeparam>
    public interface IDateField<out TResult> : IDateField, ITextField<TResult>
        where TResult : IBlock
    {
        TResult EnterDate(System.DateTime date);

        TResult EnterDateFirstDayOfPreviousQuarter(System.DateTime previousDate);

        TResult EnterDateOffset(System.DateTime DateToday, int offset);

        TResult EnterDateLastDayOfYear(System.DateTime currentDate);

        TResult EnterDateFirstDayOfYear(System.DateTime currentDate);

        TResult EnterDateToday(System.DateTime currentDate);

        TResult EnterPreviousYearOffset(System.DateTime DateToday, int offset);

        TResult EnterNextYearOffset(System.DateTime DateToday, int offset);
    }
}