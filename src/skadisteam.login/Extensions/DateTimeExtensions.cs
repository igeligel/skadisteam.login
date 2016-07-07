using System;

namespace skadisteam.login.Extensions
{
    /// <summary>
    /// Class to extend functionality of the DateTime type.
    /// </summary>
    internal static class DateTimeExtensions
    {
        /// <summary>
        /// Method to convert a date time object to the unix timestamp.
        /// It is the millisecond type of the timestamp.
        /// </summary>
        /// <param name="dateTimeInstance">Instance of the DateTime type.</param>
        /// <returns>
        /// A long containing the unix timestamp in millisecond format.
        /// </returns>
        internal static long ToUnixMilliSecondTime(this DateTime dateTimeInstance)
        {
            var duration = dateTimeInstance - new DateTime(1970, 1, 1, 0, 0, 0);

            return (long)(1000 * duration.TotalSeconds);
        }
    }
}
