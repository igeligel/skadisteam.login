using skadisteam.login.Extensions;
using Xunit;

namespace skadisteam.login.test.Extensions.Tests
{
    public class CharTest
    {
        [Fact]
        public void CheckCharacterSmallT()
        {
            Assert.Equal(61, 't'.GetHexVal());
        }

        [Fact]
        public void CheckCharacterLatinSmallLetterA_WithRingAbove()
        {
            Assert.Equal(65478, '�'.GetHexVal());
        }
    }
}
