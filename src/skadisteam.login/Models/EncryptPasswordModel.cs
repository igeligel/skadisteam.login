namespace skadisteam.login.Models
{
    public class EncryptPasswordModel
    {
        public string Password { get; set; }
        public string PublicKeyExp { get; set; }
        public string PublicKeyMod { get; set; }
    }
}
