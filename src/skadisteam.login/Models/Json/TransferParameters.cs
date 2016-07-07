using Newtonsoft.Json;

namespace skadisteam.login.Models.Json
{
    /// <summary>
    /// Transfer parameters given by steam.
    /// This is the transfer route to initialize the 
    /// cookies and finish the login process.
    /// </summary>
    internal class TransferParameters
    {
        /// <summary>
        /// Auth value given by steam.
        /// </summary>
        [JsonProperty(PropertyName = "auth")]
        internal string Auth { get; set; }
        /// <summary>
        /// Value given by steam if the server remembers the
        /// login.
        /// </summary>
        [JsonProperty(PropertyName = "remember_login")]
        internal bool RememberLogin { get; set; }
        /// <summary>
        /// Steam community id of the account which logged in.
        /// </summary>
        [JsonProperty(PropertyName = "steamid")]
        internal string SteamId { get; set; }
        /// <summary>
        /// Value of the token sent by Steam.
        /// </summary>
        [JsonProperty(PropertyName = "token")]
        internal string Token { get; set; }
        /// <summary>
        /// Token which adds additional security level.
        /// </summary>
        [JsonProperty(PropertyName = "token_secure")]
        internal string TokenSecure { get; set; }
        /// <summary>
        /// WebCookie of Steam.
        /// </summary>
        [JsonProperty(PropertyName = "webcookie")]
        internal string WebCookie { get; set; }
    }
}
