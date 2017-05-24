using skadisteam.login.Models.Json;
using skadisteam.shared.Models;

namespace skadisteam.login.Factories
{
    internal static class SkadiLoginErrorFactory
    {
        internal static SkadiLoginError Create(DoLoginResponse doLoginResponse)
        {
            SkadiLoginError skadiLoginError = new SkadiLoginError();
            if (doLoginResponse.CaptchaNeeded)
            {
                skadiLoginError.CaptchaNeeded =
                    doLoginResponse.CaptchaNeeded;
                skadiLoginError.CaptchaGid = doLoginResponse.CaptchaGid;
                skadiLoginError.Message = doLoginResponse.Message;
                skadiLoginError.Type = ErrorType.CaptchaNeeded;
            }
            else if (doLoginResponse.RequiresTwoFactor)
            {
                skadiLoginError.CaptchaNeeded =
                    doLoginResponse.CaptchaNeeded;
                skadiLoginError.CaptchaGid = doLoginResponse.CaptchaGid;
                skadiLoginError.Message = doLoginResponse.Message;
                skadiLoginError.Type = ErrorType.TwoFactor;
            }
            else if (doLoginResponse.Message == "Incorrect login.")
            {
                skadiLoginError.CaptchaNeeded =
                    doLoginResponse.CaptchaNeeded;
                skadiLoginError.CaptchaGid = doLoginResponse.CaptchaGid;
                skadiLoginError.Message = doLoginResponse.Message;
                skadiLoginError.Type = ErrorType.IncorrectLogin;
            }
            return skadiLoginError;
        }
    }
}
