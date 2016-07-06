using skadisteam.login.Models;
using Xunit;

namespace skadisteam.login.test.Models
{
    public class SkadiLoginConfigurationTest
    {
        [Fact]
        public void TestDefaultParameters()
        {
            var skadiLoginConfiguration = new SkadiLoginConfiguration();
            Assert.True(skadiLoginConfiguration.StopOnError);
            Assert.Equal(5, skadiLoginConfiguration.WaitTimeEachError);
        }
    }
}
