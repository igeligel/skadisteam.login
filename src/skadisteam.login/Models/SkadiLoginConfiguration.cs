namespace skadisteam.login.Models
{
    /// <summary>
    /// Configuration for the Skadi Steam Login.
    /// Here you can set several behavioural patterns
    /// of the login.
    /// </summary>
    public class SkadiLoginConfiguration
    {
        /// <summary>
        /// Value on which is decided if the login should stop.
        /// </summary>
        public bool StopOnError { get; set; } = true;
        /// <summary>
        /// If an error occurs this is the wait time after it.
        /// This is there to make sure you are not sending too
        /// many requests to the Steam servers.
        /// </summary>
        public int WaitTimeEachError { get; set; } = 5;
    }
}
