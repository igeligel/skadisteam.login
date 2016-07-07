using System.Collections.Generic;
using Newtonsoft.Json;

namespace skadisteam.login.Models.Json
{
    /// <summary>
    /// Response given by Steam after requesting the /dologin endpoint.
    /// </summary>
    internal class DoLoginResponse
    {
        /// <summary>
        /// Value to show if the login was successful.
        /// </summary>
        [JsonProperty(PropertyName = "success")]
        internal bool Success { get; set; }
        /// <summary>
        /// Boolean value to show the user if twofactor
        /// is required.
        /// </summary>
        [JsonProperty(PropertyName = "requires_twofactor")]
        internal bool RequiresTwoFactor { get; set; }
        /// <summary>
        /// Value to show the user that the login was completed.
        /// </summary>
        [JsonProperty(PropertyName = "login_complete")]
        internal bool LoginComplete { get; set; }
        /// <summary>
        /// A list of transfer url's to finish the login.
        /// </summary>
        [JsonProperty(PropertyName = "transfer_urls")]
        internal List<string> TransferUrls { get; set; }
        /// <summary>
        /// Parameters which need to be used when transferring.
        /// </summary>
        [JsonProperty(PropertyName = "transfer_parameters")]
        internal TransferParameters TransferParameters { get; set; }
        /// <summary>
        /// Boolean value which describes if the password field
        /// was cleared. This can also be true if the twofactor
        /// code was wrong.
        /// </summary>
        [JsonProperty(PropertyName = "clear_password_field")]
        internal bool ClearPasswordField { get; set; }
        /// <summary>
        /// Boolean which describes if a captcha is needed.
        /// </summary>
        [JsonProperty(PropertyName = "captcha_needed")]
        internal bool CaptchaNeeded { get; set; }
        /// <summary>
        /// Id of the captcha. This is needed when you are
        /// doing too many requests to steam. With this id
        /// you can create an url to get the captcha image.
        /// </summary>
        [JsonProperty(PropertyName = "captcha_gid")]
        internal long CaptchaGid { get; set; }
        /// <summary>
        /// Message given by Steam to describe the problems.
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        internal string Message { get; set; }
    }
}
