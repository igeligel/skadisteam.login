using System.Collections.Generic;
using skadisteam.login.Extensions;
using Xunit;
using System;

namespace skadisteam.login.test.Extensions.Tests
{
    public class StringTest
    {
        [Fact]
        public void CheckHexToByte()
        {
            List<byte> expectedResult = new List<byte>
            {
                1, 159, 49, 74
            };
            Assert.Equal(expectedResult, "019F314A".HexToByte());
        }

        [Fact]
        public void CheckHexToByteError()
        {
            Exception ex = Assert.Throws<Exception>(() => "011".HexToByte());
            Assert.Equal("The binary key cannot have an odd number of digits", ex.Message);
        }
    }
}
