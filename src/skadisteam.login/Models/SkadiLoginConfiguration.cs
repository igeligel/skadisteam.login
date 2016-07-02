namespace skadisteam.login.Models
{
    public class SkadiLoginConfiguration
    {
        public bool StopOnError { get; set; } = true;
        public int WaitTimeEachError { get; set; } = 5;
    }
}
