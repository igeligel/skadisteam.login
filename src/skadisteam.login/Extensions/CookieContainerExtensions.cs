using System;
using System.Net;
using skadisteam.login.Constants;

namespace skadisteam.login.Extensions
{
    /// <summary>
    /// Class to extend functionality of the CookieContainer. 
    /// We need this to add special cookies to our CookieContainer.
    /// </summary>
    internal static class CookieContainerExtensions
    {
        /// <summary>
        /// Method to add the english steam language to the CookieContainer. It is needed
        /// to set a standard language for the package.
        /// </summary>
        /// <param name="cookieContainer">
        /// CookieContainer which the cookie should be added to.
        /// </param>
        internal static void AddEnglishSteamLanguage(this CookieContainer cookieContainer)
        {
            var steamCommunityUri = new Uri(Ressources.SteamCommmunitySecureBase);
            cookieContainer.Add(steamCommunityUri, CreateLanguageCookie(steamCommunityUri));
        }

        /// <summary>
        /// Will just create the cookie containing the steam language parameters.
        /// </summary>
        /// <param name="uri">
        /// Uri referring the urlwhich should be used for the cookie.
        /// </param>
        /// <returns>The Cookie instance containing the required information.</returns>
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
