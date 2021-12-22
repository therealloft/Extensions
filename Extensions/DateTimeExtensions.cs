using System;
using System.Data.SqlTypes;

namespace Extensions
{
    public static class DateTimeExtensions
    {
        public static readonly string UtcDateFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'";

        public static readonly DateTime SqlDateTimeMinUtc = SqlDateTime.MinValue.Value.AsUtcKind();

        /// <summary>
        /// Format Date time to yyyy-MM-ddTHH:mm:ss.fffZ
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToUtcFormatString(this DateTime date)
        {
            return date.ToUniversalTime().ToString(UtcDateFormat);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime ToSqlDateTimeMinUtc(this DateTime date)
        {
            return SqlDateTimeMinUtc;
        }
        /// <summary>
        /// Get DateTime as UTC
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static DateTime AsUtcKind(this DateTime datetime)
        {
            return DateTime.SpecifyKind(datetime, DateTimeKind.Utc);
        }
    }
}
