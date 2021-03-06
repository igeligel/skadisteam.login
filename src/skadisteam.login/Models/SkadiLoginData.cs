namespace skadisteam.login.Models
{
    /// <summary>
    /// Login Data Model which is used by the login.
    /// </summary>
    public class SkadiLoginData
    {
        /// <summary>
        /// Username of the Steam account.
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Password of the Steam account.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Shared Secret value of the Steam account.
        /// This is generated by twofactor provided.
        /// This value can also be found in the maFile
        /// of the SteamDesktopAuthenticator.
        /// </summary>
        public string SharedSecret { get; set; }
    }
}
