using System.Net;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using skadisteam.login.Constants;
using System.Linq;
using System.Net.Http;
using skadisteam.shared.Models;

namespace skadisteam.login.Factories
{
    internal static class SkadiLoginResponseFactory
    {
        internal static SkadiLoginResponse Create(HttpResponseMessage response, CookieContainer cookieContainer)
        {
            var skadiLoginResponse = new SkadiLoginResponse();
            // ReSharper disable once SuggestVarOrType_Elsewhere
            IEnumerable<Cookie> responseCookies = cookieContainer.GetCookies(Uris.SteamCommunitySecureBase).Cast<Cookie>();
            var responseUri = response.RequestMessage.RequestUri.ToString();
            var steam64Id = long.Parse(Regex.Split(Regex.Split(responseUri, "http://steamcommunity.com/profiles/")[1], "/home")[0]);
            skadiLoginResponse.SteamCommunityId = steam64Id;
            skadiLoginResponse.SteamCountry = responseCookies.FirstOrDefault(e => e.Name == "steamCountry").Value;
            skadiLoginResponse.SteamLogin = responseCookies.FirstOrDefault(e => e.Name == "steamLogin").Value;
            skadiLoginResponse.SteamRememberLogin = responseCookies.FirstOrDefault(e => e.Name == "steamRememberLogin").Value;
            skadiLoginResponse.SessionId = responseCookies.FirstOrDefault(e => e.Name == "sessionid").Value;
            skadiLoginResponse.SteamLanguage = responseCookies.FirstOrDefault(e => e.Name == "Steam_Language").Value;
            skadiLoginResponse.SteamLoginSecure =
                responseCookies.FirstOrDefault(e => e.Name == "steamLoginSecure")
                    .Value;
            var steamMachineAuthCookie =
                responseCookies.FirstOrDefault(
                    e => e.Name.Contains("steamMachineAuth"));
            skadiLoginResponse.SteamMachineAuthvalue =
                steamMachineAuthCookie.Value;
            skadiLoginResponse.SkadiLoginCookies = cookieContainer;
            return skadiLoginResponse;
        }
    }
}
