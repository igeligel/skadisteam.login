using System;
using System.Net;
using skadi_steam_login.Constants;

namespace skadi_steam_login.Extensions
{
    internal static class CookieContainerExtensions
    {
        public static void AddEnglishSteamLanguage(this CookieContainer cookieContainer)
        {
            var steamCommunityUri = new Uri(Ressources.SteamCommmunitySecureBase);
            cookieContainer.Add(steamCommunityUri, CreateLanguageCookie(steamCommunityUri));
        }

        private static Cookie CreateLanguageCookie(Uri uri)
        {
            Cookie steamLanguageCookie = new Cookie("Steam_Language", "english")
            {
                Domain = uri.Host
            };
            return steamLanguageCookie;
        } 
    }
}
