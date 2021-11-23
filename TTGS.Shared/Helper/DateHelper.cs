using System;

namespace TTGS.Shared.Helper
{
    public static class DateHelper
    {
        public static DateTime FirstDayOfWeek(this DateTime value)
        {
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var diff = value.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
            if (diff < 0)
                diff += 7;
            return value.AddDays(-diff).Date;
        }

        public static DateTime FirstDayOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, 1);
        }
    }
}
