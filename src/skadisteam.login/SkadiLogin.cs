using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using skadisteam.login.Constants;
using skadisteam.login.Http.Headers;
using skadisteam.login.Models;
using skadisteam.login.Http;
using skadisteam.login.Models.Json;
using skadisteam.login.Factories;
using skadisteam.login.Validators;

namespace skadisteam.login
{
    public class SkadiLogin
    {
        private RequestFactory _requestFactory;
        private SkadiLoginConfiguration _skadiLoginConfiguration;

        public SkadiLogin()
        {
            _requestFactory = new RequestFactory();
        }

        public SkadiLogin(SkadiLoginConfiguration skadiLoginConfiguration)
        {
            _skadiLoginConfiguration = skadiLoginConfiguration;
        }
        
        public SkadiLoginResponse Execute(SkadiLoginData skadiLoginData)
        {
            if (!_skadiLoginConfiguration.StopOnError)
            {
                ExecuteEndless(skadiLoginData);
            }
            var rsaKey = GetRsaKey(skadiLoginData.Username);
            var doLoginResponse = DoLogin(rsaKey, skadiLoginData.Username,
                skadiLoginData.Password, skadiLoginData.SharedSecret);
            
            if (!DoLoginResponseValidator.IsValid(doLoginResponse))
            {
                SkadiLoginResponse skadiLoginResponse = new SkadiLoginResponse();
                skadiLoginResponse.SkadiLoginError =
                    SkadiLoginErrorFactory.Create(doLoginResponse);
                return skadiLoginResponse;
            }
            Transfer(doLoginResponse);
            return SetSession();
        }

        private SkadiLoginResponse ExecuteEndless(SkadiLoginData skadiLoginData)
        {
            GetRsaKeyResponse rsaKey = null;
            DoLoginResponse doLoginResponse = null;

            while (rsaKey == null || doLoginResponse == null)
            {
                try
                {
                    rsaKey = GetRsaKey(skadiLoginData.Username);
                    if (rsaKey == null) continue;
                    doLoginResponse = DoLogin(rsaKey, skadiLoginData.Username,
                        skadiLoginData.Password, skadiLoginData.SharedSecret);
                }
                catch (Exception)
                {
                    Task.Delay(
                        TimeSpan.FromSeconds(
                            _skadiLoginConfiguration.WaitTimeEachError)).Wait();
                }
            }

            return null;
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
