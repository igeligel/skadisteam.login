using Newtonsoft.Json;
using skadisteam.login.Constants;
using skadisteam.login.Http.Headers;
using skadisteam.login.Models;
using skadisteam.login.Http;
using skadisteam.login.Models.Json;
using skadisteam.login.Factories;
using skadi_steam_login.Validators;
using skadi_steam_login.Models;

namespace skadisteam.login
{
    public class SkadiLogin
    {
        private RequestFactory _requestFactory;

        public SkadiLogin()
        {
            _requestFactory = new RequestFactory();
        }
        
        public SkadiLoginResponse Execute(SkadiLoginData skadiLoginData)
        {
            var rsaKey = GetRsaKey(skadiLoginData.Username);
            var doLoginResponse = DoLogin(rsaKey, skadiLoginData.Username,
                skadiLoginData.Password, skadiLoginData.SharedSecret);

            // Validate Response.
            if (!DoLoginResponseValidator.IsValid(doLoginResponse))
            {
                SkadiLoginResponse skadiLoginResponse = new SkadiLoginResponse();
                SkadiLoginError skadiLoginError = new SkadiLoginError();
                // ERROR HANDLING AND RETURN OR RECURSIVE
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
                skadiLoginResponse.SkadiLoginError = skadiLoginError;
                return skadiLoginResponse;
            }
            Transfer(doLoginResponse);
            return SetSession();
        }

        private GetRsaKeyResponse GetRsaKey(string username)
        {
            var postContent = PostDataFactory.CreateGetRsaKeyData(username);
            GetRsaKeyResponse getRsaKeyResponse = null;

            var response = _requestFactory.Create(HttpMethod.POST,
                Uris.SteamCommunitySecureBase,
                SteamCommunityEndpoints.GetRsaKey, Accept.All,
                HttpHeaderValues.AcceptLanguageOne, false, true, true, true,
                false, postContent);
            string responseContent = response.Content.ReadAsStringAsync().Result;
            getRsaKeyResponse =
                JsonConvert.DeserializeObject<GetRsaKeyResponse>(responseContent);
            return getRsaKeyResponse;
        }

        private DoLoginResponse DoLogin(GetRsaKeyResponse getRsaKeyResponse,
            string username, string password, string sharedSecret)
        {
            DoLoginResponse doLoginResponse = null;
            var encryptedPassword =
                EncryptPasswordFactory.Create(getRsaKeyResponse, password);
            var content = PostDataFactory.CreateDoLoginData(username,
                encryptedPassword, getRsaKeyResponse.Timestamp,
                TwoFactorCodeFactory.Create(sharedSecret));

            var response = _requestFactory.Create(HttpMethod.POST,
                Uris.SteamCommunitySecureBase,
                SteamCommunityEndpoints.DoLogin, Accept.All,
                HttpHeaderValues.AcceptLanguageOne, false, true, true, true,
                false, content);

            string responseContent = response.Content.ReadAsStringAsync().Result;
            doLoginResponse =
                JsonConvert.DeserializeObject<DoLoginResponse>(responseContent);
            return doLoginResponse;
        }
        
        private void Transfer(DoLoginResponse doLoginResponse)
        {
            var content = PostDataFactory.CreateTransferData(doLoginResponse);
            _requestFactory.Create(HttpMethod.POST,
                Uris.HelpSteampoweredSecureBase,
                HelpSteampoweredEndpoints.TransferLogin,
                Accept.Html, HttpHeaderValues.AcceptLanguageTwo, true, true,
                true, false, true, content);
        }

        private SkadiLoginResponse SetSession()
        {
            var response = _requestFactory.Create(HttpMethod.GET,
                Uris.SteamCommunityBase, SteamCommunityEndpoints.Home,
                Accept.Html, HttpHeaderValues.AcceptLanguageTwo, true, false,
                false, false, false, null);
            return SkadiLoginResponseFactory.Create(response, _requestFactory.GetCookieContainer());
        }
    }
}
