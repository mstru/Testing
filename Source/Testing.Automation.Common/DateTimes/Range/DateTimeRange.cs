using Testing.Automation.Common.DateTimes.Extensions;
using System;

namespace Testing.Automation.Common.DateTimes.Range
{
    public static class DateTimeRange
    {
        public static string DocumentIntervalWithUtc(this System.DateTime date, string format, int DateOffset1, int DateOffset2)
        {
            var DocumentInterval1 = DateTimeExtensions.NextDay(date, DateOffset1);
            var DocumentInterval2 = DateTimeExtensions.NextDay(date, DateOffset2);

            var Utc1 = new DateTimeOffset(DocumentInterval1).UtcDateTime.Hour;
            var Utc2 = new DateTimeOffset(DocumentInterval2).UtcDateTime.Hour;

            return (DocumentInterval1.ToString(format) + "T" + Utc1 + ":00Z" + "/" + DocumentInterval2.ToString(format) + "T" + Utc2 + ":00Z");
        }

        public static string TimeStampInterval(this System.DateTime date, string format, int DateOffset1, int DateOffset2)
        {
            var TimeStampInterval1 = DateTimeExtensions.NextDay(date, DateOffset1);
            var TimeStampInterval2 = DateTimeExtensions.NextDay(date, DateOffset2);

            return (TimeStampInterval1.ToString(format) + "_" + TimeStampInterval2.ToString(format));
        }

        //cesta nahradna vymena
        public static string TimeStampIntervalPathAE(int offset)
        {
            string date = DateTimeExtensions.NextDay(System.DateTime.Today, offset).ToString("yy MM dd");
            return date.Replace(" ", @"\");
        }
    }
}
