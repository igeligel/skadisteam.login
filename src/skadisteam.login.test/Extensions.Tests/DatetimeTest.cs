using skadisteam.login.Extensions;
using System;
using Xunit;

namespace skadisteam.login.test.Extensions.Tests
{
    public class DateTimeTest
    {
        [Fact]
        public void CheckDate()
        {
            DateTime dateTime = new DateTime(2016, 5, 3, 15, 42, 47, DateTimeKind.Utc);
            Assert.Equal(dateTime.ToUnixMilliSecondTime(), 1462290167000);
        }

        [Fact]
        public void CheckInvalid()
        {
            DateTime dateTime = new DateTime(1963, 5, 3, 15, 42, 47, DateTimeKind.Utc);
            var test = dateTime.ToUnixMilliSecondTime();
            Assert.InRange(test, long.MinValue, -1);
        }
    }
}
