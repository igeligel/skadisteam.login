using skadi_steam_login.Models;
using skadi_steam_login.Models.Json;

namespace skadi_steam_login.Factories
{
    public static class EncryptPasswordFactory
    {
        public static EncryptPasswordModel Create(GetRsaKeyResponse getRsaKeyResponse, string password)
        {
            EncryptPasswordModel encryptPasswordModel = new EncryptPasswordModel();
            encryptPasswordModel.PublicKeyExp = getRsaKeyResponse.PublicKeyExp;
            encryptPasswordModel.PublicKeyMod = getRsaKeyResponse.PublicKeyMod;
            encryptPasswordModel.Password = password;
            return encryptPasswordModel;
        }
    }
}
