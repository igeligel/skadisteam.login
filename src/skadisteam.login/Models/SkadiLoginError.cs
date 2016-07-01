using skadisteam.login.Models.Json;

namespace skadi_steam_login.Models
{
    public class SkadiLoginError
    {
        public ErrorType Type { get; set; }
        public bool CaptchaNeeded { get; set; }
        public long CaptchaGid { get; set; }
        public string Message { get; set; }
    }
}
