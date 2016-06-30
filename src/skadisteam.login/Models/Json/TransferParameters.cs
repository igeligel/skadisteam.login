using Newtonsoft.Json;

namespace skadi_steam_login.Models.Json
{
    public class TransferParameters
    {
        [JsonProperty(PropertyName = "steamid")]
        public string SteamId { get; set; }
        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }
        [JsonProperty(PropertyName = "auth")]
        public string Auth { get; set; }
        [JsonProperty(PropertyName = "remember_login")]
        public bool RememberLogin { get; set; }
        [JsonProperty(PropertyName = "webcookie")]
        public string WebCookie { get; set; }
        [JsonProperty(PropertyName = "token_secure")]
        public string TokenSecure { get; set; }
    }
}
