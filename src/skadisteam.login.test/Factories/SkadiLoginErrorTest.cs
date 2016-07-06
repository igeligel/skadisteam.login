using System.Net.Http;
using skadisteam.login.Factories;
using skadisteam.login.Models.Json;
using Xunit;

namespace skadisteam.login.test.Factories
{
    public class SkadiLoginErrorTest
    {
        [Fact]
        public void CaptchaNeeded()
        {
            DoLoginResponse doLoginResponse = new DoLoginResponse();
            doLoginResponse.CaptchaNeeded = true;
            doLoginResponse.CaptchaGid = 677901403761260;
            doLoginResponse.Message = "";
            doLoginResponse.RequiresTwoFactor = false;

            var skadiLoginError = SkadiLoginErrorFactory.Create(doLoginResponse);
            Assert.Equal(ErrorType.CaptchaNeeded, skadiLoginError.Type);
        }

        [Fact]
        public void RequiresTwoFactor()
        {
            DoLoginResponse doLoginResponse = new DoLoginResponse();
            doLoginResponse.CaptchaNeeded = false;
            doLoginResponse.CaptchaGid = -1;
            doLoginResponse.Message = "";
            doLoginResponse.RequiresTwoFactor = true;
            SkadiLoginErrorFactory.Create(doLoginResponse);

            var skadiLoginError = SkadiLoginErrorFactory.Create(doLoginResponse);
            Assert.Equal(ErrorType.TwoFactor, skadiLoginError.Type);
        }

        [Fact]
        public void IncorrectLogin()
        {
            DoLoginResponse doLoginResponse = new DoLoginResponse();
            doLoginResponse.CaptchaNeeded = false;
            doLoginResponse.CaptchaGid = -1;
            doLoginResponse.Message = "Incorrect login.";
            doLoginResponse.RequiresTwoFactor = false;

            var skadiLoginError = SkadiLoginErrorFactory.Create(doLoginResponse);
            Assert.Equal(ErrorType.IncorrectLogin, skadiLoginError.Type);
        }
    }
}
