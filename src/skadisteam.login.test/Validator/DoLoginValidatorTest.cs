using skadisteam.login.Models.Json;
using skadisteam.login.Validators;
using Xunit;

namespace skadisteam.login.test.Validator
{
    public class DoLoginValidatorTest
    {
        [Fact]
        public void CheckValid()
        {
            DoLoginResponse doLoginResponse = new DoLoginResponse();
            doLoginResponse.Success = true;
            doLoginResponse.LoginComplete = true;
            Assert.True(DoLoginResponseValidator.IsValid(doLoginResponse));
        }

        [Fact]
        public void CheckNotSuccessful()
        {
            DoLoginResponse doLoginResponse = new DoLoginResponse();
            doLoginResponse.Success = false;
            doLoginResponse.LoginComplete = true;
            Assert.False(DoLoginResponseValidator.IsValid(doLoginResponse));
        }

        [Fact]
        public void CheckNotCompleted()
        {
            DoLoginResponse doLoginResponse = new DoLoginResponse();
            doLoginResponse.Success = true;
            doLoginResponse.LoginComplete = false;
            Assert.False(DoLoginResponseValidator.IsValid(doLoginResponse));
        }
    }
}
