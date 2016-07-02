using skadi_steam_login.Models;
using skadisteam.login.Models.Json;

namespace skadi_steam_login.Factories
{
    public static class SkadiLoginErrorFactory
    {
        public static SkadiLoginError Create(DoLoginResponse doLoginResponse)
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
