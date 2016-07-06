using Xunit;

namespace skadisteam.login.test.Models
{
    public class SkadiLoginConfiguration
    {
        [Fact]
        public void TestDefaultParameters()
        {
            var skadiLoginConfiguration =
                new login.Models.SkadiLoginConfiguration();
            Assert.True(skadiLoginConfiguration.StopOnError);
            Assert.Equal(5, skadiLoginConfiguration.WaitTimeEachError);
        }
    }
}
