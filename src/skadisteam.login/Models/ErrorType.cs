namespace skadisteam.login.Models.Json
{
    /// <summary>
    /// Enumeration to describe errors of the login.
    /// </summary>
    public enum ErrorType
    {
        TwoFactor,
        IncorrectLogin,
        CaptchaNeeded
    }
}
