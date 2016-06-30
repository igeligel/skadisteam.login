using Newtonsoft.Json;
using skadi_steam_login.TwoFactor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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
        private HttpClientHandler _handler;

        public SkadiLogin()
        {
            _cookieContainer = new CookieContainer(); 
        }
        
        public SkadiLoginResponse Execute(SkadiLoginData skadiLoginData)
        {
            _cookieContainer = new CookieContainer();
            _cookieContainer.AddEnglishSteamLanguage();

            var rsaKey = GetRsaKey(skadiLoginData.Username);
            var doLoginResponse = DoLogin(rsaKey, skadiLoginData.Username, skadiLoginData.Password, skadiLoginData.SharedSecret);
            Transfer(doLoginResponse);
            var skadiLoginResponse = SetSession();

            skadiLoginResponse.SkadiLoginCookies = _cookieContainer;
            return skadiLoginResponse;
        }

        private List<KeyValuePair<string, string>> CreateGetRsaKeyContent(string username)
        {
            var content = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(PostParameters.Username, username),
                new KeyValuePair<string, string>(PostParameters.DoNotCache, DateTime.UtcNow.ToUnixTime().ToString())
            };
            return content;
        }

        public GetRsaKeyResponse GetRsaKey(string username)
        {
            var postContent = CreateGetRsaKeyContent(username);
            GetRsaKeyResponse getRsaKeyResponse = null;

            var response = Request(HttpMethod.POST, Uris.SteamCommmunitySecureBase, SteamCommunityEndpoints.GetRsaKey, Accept.All, HttpHeaderValues.AcceptLanguageOne,
                false, true, true, true, false, postContent);
            string responseContent = response.Content.ReadAsStringAsync().Result;
            getRsaKeyResponse = JsonConvert.DeserializeObject<GetRsaKeyResponse>(responseContent);
            return getRsaKeyResponse;
        }

        private string EncryptPassword(EncryptPasswordModel encryptPasswordModel)
        {
            var rsa = new RSACryptoServiceProvider();

            var rsaParameters = new RSAParameters
            {
                Exponent = encryptPasswordModel.PublicKeyExp.HexToByte(),
                Modulus = encryptPasswordModel.PublicKeyMod.HexToByte()
            };
            rsa.ImportParameters(rsaParameters);
            var bytePassword = Encoding.ASCII.GetBytes(encryptPasswordModel.Password);
            var encodedPassword = rsa.Encrypt(bytePassword, false);
            return Convert.ToBase64String(encodedPassword);
        }

        public DoLoginResponse DoLogin(GetRsaKeyResponse getRsaKeyResponse, string username, string password, string sharedSecret)
        {
            DoLoginResponse doLoginResponse = null;
            var encryptedBase64Password = EncryptPassword(EncryptPasswordFactory.Create(getRsaKeyResponse, password));


            var rsaTimeStamp = Uri.EscapeDataString(getRsaKeyResponse.Timestamp.ToString());
            var twoFactorGenerator = new SteamTwoFactorGenerator
            {
                SharedSecret = sharedSecret
            };
            var twoFactorCode = twoFactorGenerator.GenerateSteamGuardCodeForTime();

            var content = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(PostParameters.Username, username),
                new KeyValuePair<string, string>(PostParameters.Password, encryptedBase64Password),
                new KeyValuePair<string, string>(PostParameters.CaptchaGid, "-1"),
                new KeyValuePair<string, string>(PostParameters.CaptchaText, ""),
                new KeyValuePair<string, string>(PostParameters.RememberLogin, "true"),
                new KeyValuePair<string, string>(PostParameters.LoginFriendlyName, "skadi-steam-login"),
                new KeyValuePair<string, string>(PostParameters.EmailAuth, ""),
                new KeyValuePair<string, string>(PostParameters.EmailSteamId, ""),
                new KeyValuePair<string, string>(PostParameters.RsaTimestamp, getRsaKeyResponse.Timestamp),
                new KeyValuePair<string, string>(PostParameters.DoNotCache, DateTime.UtcNow.ToUnixTime().ToString()),
                new KeyValuePair<string, string>(PostParameters.TwoFactorCode, twoFactorCode)
            };

            var response = Request(HttpMethod.POST, new Uri("https://steamcommunity.com"), SteamCommunityEndpoints.DoLogin, Accept.All,
                HttpHeaderValues.AcceptLanguageOne,
                false, true, true, true, false, content);

            string responseContent = response.Content.ReadAsStringAsync().Result;
            doLoginResponse = JsonConvert.DeserializeObject<DoLoginResponse>(responseContent);
            return doLoginResponse;
        }

        public void Transfer(DoLoginResponse doLoginResponse)
        {
            var content = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(PostParameters.SteamId, doLoginResponse.TransferParameters.SteamId),
                new KeyValuePair<string, string>(PostParameters.Token, doLoginResponse.TransferParameters.Token),
                new KeyValuePair<string, string>(PostParameters.Auth, doLoginResponse.TransferParameters.Auth),
                new KeyValuePair<string, string>(PostParameters.RememberLogin, doLoginResponse.TransferParameters.RememberLogin.ToString()),
                new KeyValuePair<string, string>(PostParameters.TokenSecure, doLoginResponse.TransferParameters.TokenSecure),
            };
            var response = Request(HttpMethod.POST, new Uri("https://help.steampowered.com"), "/login/transfer",
                Accept.Html,
                HttpHeaderValues.AcceptLanguageTwo, true, true, true, false, true, content);
        }

        public SkadiLoginResponse SetSession()
        {
            var skadiLoginResponse = new SkadiLoginResponse();
            var steamCommunityUri = new Uri("http://steamcommunity.com");
            var response = Request(HttpMethod.GET, steamCommunityUri, "/my/home",
                Accept.Html, HttpHeaderValues.AcceptLanguageTwo, true, false, false, false, false, null);
            response.EnsureSuccessStatusCode();
            IEnumerable<Cookie> responseCookies = _cookieContainer.GetCookies(steamCommunityUri).Cast<Cookie>();
            string responseUri = response.RequestMessage.RequestUri.ToString();
            var steam64Id = long.Parse(Regex.Split(Regex.Split(responseUri, "http://steamcommunity.com/profiles/")[1], "/home")[0]);
            skadiLoginResponse.SteamCommunityId = steam64Id;

            skadiLoginResponse.SteamCountry = responseCookies.FirstOrDefault(e => e.Name == "steamCountry").Value;
            skadiLoginResponse.SteamLogin = responseCookies.FirstOrDefault(e => e.Name == "steamLogin").Value;
            skadiLoginResponse.SteamRememberLogin = responseCookies.FirstOrDefault(e => e.Name == "steamRememberLogin").Value;
            skadiLoginResponse.SessionId = responseCookies.FirstOrDefault(e => e.Name == "sessionid").Value;
            skadiLoginResponse.SteamLanguage = responseCookies.FirstOrDefault(e => e.Name == "Steam_Language").Value;
            return skadiLoginResponse;
        }

        public HttpResponseMessage Request(HttpMethod method, Uri uri, string path, Accept acceptHeader, string acceptLanguage, bool upgradeInsecureRequests,
            bool steamCommunityOriginSet, bool steamCommunityRefererSet, bool isXmlHttpRequest, bool cacheControlSet, IEnumerable<KeyValuePair<string, string>> postContent)
        {
            HttpResponseMessage response = null;
            _handler = new HttpClientHandler();
            _handler.CookieContainer = _cookieContainer;
            _handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            using (var client = new HttpClient(_handler))
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Add(HttpHeaderKeys.AcceptEncoding, "gzip, deflate");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Language", acceptLanguage);
                client.DefaultRequestHeaders.Add(HttpHeaderKeys.UserAgent, "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36");
                client.DefaultRequestHeaders.Host = uri.Host;

                if (acceptHeader == Accept.All)
                {
                    client.DefaultRequestHeaders.TryAddWithoutValidation(HttpHeaderKeys.Accept, "*/*");
                }
                else if (acceptHeader == Accept.Html)
                {
                    client.DefaultRequestHeaders.TryAddWithoutValidation(HttpHeaderKeys.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                }

                if (steamCommunityOriginSet)
                {
                    client.DefaultRequestHeaders.Add(HttpHeaderKeys.Origin, Ressources.SteamCommmunitySecureBase);
                }

                if (cacheControlSet)
                {
                    client.DefaultRequestHeaders.Add(HttpHeaderKeys.CacheControl, "max-age=0");
                }

                if (isXmlHttpRequest)
                {
                    client.DefaultRequestHeaders.Add(HttpHeaderKeys.XRequestedWith, "XMLHttpRequest");
                }

                if (steamCommunityRefererSet)
                {
                    client.DefaultRequestHeaders.Add(HttpHeaderKeys.Referer, "https://steamcommunity.com/login/home/?goto=0");
                }

                if (upgradeInsecureRequests)
                {
                    client.DefaultRequestHeaders.Add(HttpHeaderKeys.UpgradeInsecureRequests, "1");
                }

                if (method == HttpMethod.POST)
                {
                    var content = new FormUrlEncodedContent(postContent);
                    response = client.PostAsync(path, content).Result;
                }
                else if (method == HttpMethod.GET)
                {
                    response = client.GetAsync(path).Result;
                }

            }
            return response;
        }
    }
}
