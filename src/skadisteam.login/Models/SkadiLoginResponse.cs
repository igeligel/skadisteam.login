using System.Net;
using skadisteam.login.Models;

namespace skadisteam.login.Models
{
    public class SkadiLoginResponse
    {
        public string SessionId { get; set; }
        public string SteamCountry { get; set; }
        public string SteamLogin { get; set; }
        public string SteamRememberLogin { get; set; }
        public string SteamLanguage { get; set; }
        public long SteamCommunityId { get; set; }
        public CookieContainer SkadiLoginCookies { get; set; }
        public SkadiLoginError SkadiLoginError { get; set; }
    }
}
