using System.Net;

namespace skadisteam.login.Models
{
    /// <summary>
    /// Response which is given of the response.
    /// This is used for interacting with other services.
    /// </summary>
    public class SkadiLoginResponse
    {
        /// <summary>
        /// Id of your session.
        /// </summary>
        public string SessionId { get; set; }
        /// <summary>
        /// Country which is given by steam.
        /// </summary>
        public string SteamCountry { get; set; }
        /// <summary>
        /// Steam login value.
        /// </summary>
        public string SteamLogin { get; set; }
        /// <summary>
        /// Steam remember login value.
        /// </summary>
        public string SteamRememberLogin { get; set; }
        /// <summary>
        /// Steam language which is set. Should be english.
        /// </summary>
        public string SteamLanguage { get; set; }
        /// <summary>
        /// The steam community id of the account which logged in.
        /// </summary>
        public long SteamCommunityId { get; set; }
        /// <summary>
        /// The CookieContainer which is used for the login.
        /// </summary>
        public CookieContainer SkadiLoginCookies { get; set; }
        /// <summary>
        /// Error instance which is given if the login fails.
        /// See <see cref="SkadiLoginError"/> for more information.
        /// </summary>
        public SkadiLoginError SkadiLoginError { get; set; }
    }
}
