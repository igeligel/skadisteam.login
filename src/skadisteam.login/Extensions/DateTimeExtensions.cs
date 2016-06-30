using System;

namespace skadisteam.login.Extensions
{
    internal static class DateTimeExtensions
    {
        public static long ToUnixTime(this DateTime input)
        {
            var duration = input - new DateTime(1970, 1, 1, 0, 0, 0);

            return (long)(1000 * duration.TotalSeconds);
        }
    }
}
