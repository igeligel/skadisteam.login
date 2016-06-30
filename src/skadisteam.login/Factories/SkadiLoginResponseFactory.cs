using System.Net;
using skadi_steam_login.Models;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using skadi_steam_login.Constants;
using System.Linq;
using System.Net.Http;

namespace skadi_steam_login.Factories
{
    public static class SkadiLoginResponseFactory
    {
        public static SkadiLoginResponse Create(HttpResponseMessage response, CookieContainer cookieContainer)
        {
            SkadiLoginResponse skadiLoginResponse = new SkadiLoginResponse();
            IEnumerable<Cookie> responseCookies = cookieContainer.GetCookies(Uris.SteamCommunitySecureBase).Cast<Cookie>();
            string responseUri = response.RequestMessage.RequestUri.ToString();
            var steam64Id = long.Parse(Regex.Split(Regex.Split(responseUri, "http://steamcommunity.com/profiles/")[1], "/home")[0]);
            skadiLoginResponse.SteamCommunityId = steam64Id;
            skadiLoginResponse.SteamCountry = responseCookies.FirstOrDefault(e => e.Name == "steamCountry").Value;
            skadiLoginResponse.SteamLogin = responseCookies.FirstOrDefault(e => e.Name == "steamLogin").Value;
            skadiLoginResponse.SteamRememberLogin = responseCookies.FirstOrDefault(e => e.Name == "steamRememberLogin").Value;
            skadiLoginResponse.SessionId = responseCookies.FirstOrDefault(e => e.Name == "sessionid").Value;
            skadiLoginResponse.SteamLanguage = responseCookies.FirstOrDefault(e => e.Name == "Steam_Language").Value;
            skadiLoginResponse.SkadiLoginCookies = cookieContainer;
            return skadiLoginResponse;
        }
    }
}
