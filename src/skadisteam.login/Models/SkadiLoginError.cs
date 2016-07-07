using skadisteam.login.Models.Json;

namespace skadisteam.login.Models
{
    /// <summary>
    /// Error which is probably given by the login.
    /// It will contain several information about
    /// why the login failed.
    /// </summary>
    public class SkadiLoginError
    {
        /// <summary>
        /// Type of the error. For further information
        /// lookup <see cref="ErrorType"/>.
        /// </summary>
        public ErrorType Type { get; set; }
        /// <summary>
        /// Value which is describing if a captcha is needed.
        /// </summary>
        public bool CaptchaNeeded { get; set; }
        /// <summary>
        /// Contains the captcha id if a captcha is needed.
        /// If no captcha is needed it will be -1.
        /// </summary>
        public long CaptchaGid { get; set; }
        /// <summary>
        /// Message given by steam why the login failed.
        /// </summary>
        public string Message { get; set; }
    }
}
