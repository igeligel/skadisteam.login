namespace skadisteam.login.Models.Json
{
    /// <summary>
    /// Enumeration to describe errors of the login.
    /// </summary>
    public enum ErrorType
    {
        /// <summary>
        /// Given when the twofactor code is wrong.
        /// </summary>
        TwoFactor,
        /// <summary>
        /// Given when the combination of username and
        /// password is incorrect.
        /// </summary>
        IncorrectLogin,
        /// <summary>
        /// Given when there were to many failed attempts
        /// to login.
        /// </summary>
        CaptchaNeeded
    }
}
