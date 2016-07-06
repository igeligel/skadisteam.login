using skadisteam.login.Extensions;
using skadisteam.login.Constants;
using System.Net;
using Xunit;

namespace skadisteam.login.test.Extensions
{
    public class CookieContainerTest
    {
        [Fact]
        public void CheckAddedSteamLanguageCookie()
        {
            CookieContainer cookieContainer = new CookieContainer();
            cookieContainer.AddEnglishSteamLanguage();
            Assert.Equal(1, cookieContainer.Count);
        }

        [Fact]
        public void CheckNameSteamLanguageCookie()
        {
            CookieContainer cookieContainer = new CookieContainer();
            cookieContainer.AddEnglishSteamLanguage();
            var test = cookieContainer.GetCookies(Uris.SteamCommunitySecureBase);
            Assert.Equal("Steam_Language", test["Steam_Language"].Name);
        }

        [Fact]
        public void CheckValueSteamLanguageCookie()
        {
            CookieContainer cookieContainer = new CookieContainer();
            cookieContainer.AddEnglishSteamLanguage();
            var test = cookieContainer.GetCookies(Uris.SteamCommunitySecureBase);
            Assert.Equal("english", test["Steam_Language"].Value);
        }

        [Fact]
        public void CheckDomainSteamLanguageCookie()
        {
            CookieContainer cookieContainer = new CookieContainer();
            cookieContainer.AddEnglishSteamLanguage();
            var test = cookieContainer.GetCookies(Uris.SteamCommunitySecureBase);
            Assert.Equal("steamcommunity.com", test["Steam_Language"].Domain);
        }
    }
}
