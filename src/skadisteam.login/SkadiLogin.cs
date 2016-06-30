using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using skadi_steam_login.Constants;
using skadi_steam_login.Extensions;
using skadi_steam_login.Http.Headers;
using skadi_steam_login.Models;
using HttpMethod = skadi_steam_login.Http.HttpMethod;
using skadi_steam_login.Models.Json;
using skadi_steam_login.Factories;

namespace skadi_steam_login
{
    public class SkadiLogin
    {
        private CookieContainer _cookieContainer;

        public SkadiLogin()
        {
            _cookieContainer = new CookieContainer(); 
        }
        
        public SkadiLoginResponse Execute(SkadiLoginData skadiLoginData)
        {
            _cookieContainer = new CookieContainer();
            _cookieContainer.AddEnglishSteamLanguage();

            var rsaKey = GetRsaKey(skadiLoginData.Username);
            var doLoginResponse = DoLogin(rsaKey, skadiLoginData.Username,
                skadiLoginData.Password, skadiLoginData.SharedSecret);
            Transfer(doLoginResponse);
            return SetSession();
        }

        private GetRsaKeyResponse GetRsaKey(string username)
        {
            var postContent = PostDataFactory.CreateGetRsaKeyData(username);
            GetRsaKeyResponse getRsaKeyResponse = null;

            var response = RequestFactory.Create(HttpMethod.POST,
                Uris.SteamCommunitySecureBase,
                SteamCommunityEndpoints.GetRsaKey, Accept.All,
                HttpHeaderValues.AcceptLanguageOne, false, true, true, true,
                false, postContent, _cookieContainer);
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

            var response = RequestFactory.Create(HttpMethod.POST,
                Uris.SteamCommunitySecureBase,
                SteamCommunityEndpoints.DoLogin, Accept.All,
                HttpHeaderValues.AcceptLanguageOne, false, true, true, true,
                false, content, _cookieContainer);

            string responseContent = response.Content.ReadAsStringAsync().Result;
            doLoginResponse =
                JsonConvert.DeserializeObject<DoLoginResponse>(responseContent);
            return doLoginResponse;
        }
        
        private void Transfer(DoLoginResponse doLoginResponse)
        {
            var content = PostDataFactory.CreateTransferData(doLoginResponse);
            RequestFactory.Create(HttpMethod.POST,
                Uris.HelpSteampoweredSecureBase, "/login/transfer",
                Accept.Html, HttpHeaderValues.AcceptLanguageTwo, true, true,
                true, false, true, content, _cookieContainer);
        }

        private SkadiLoginResponse SetSession()
        {
            var response = RequestFactory.Create(HttpMethod.GET, Uris.SteamCommunityBase, "/my/home",
                Accept.Html, HttpHeaderValues.AcceptLanguageTwo, true, false,
                false, false, false, null, _cookieContainer);
            return SkadiLoginResponseFactory.Create(response, _cookieContainer);
        }
    }
}
