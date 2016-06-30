using skadi_steam_login.Extensions;
using skadi_steam_login.Models;
using skadi_steam_login.Models.Json;
using System;
using System.Security.Cryptography;
using System.Text;

namespace skadi_steam_login.Factories
{
    public static class EncryptPasswordFactory
    {
        public static string Create(GetRsaKeyResponse getRsaKeyResponse, string password)
        {
            EncryptPasswordModel encryptPasswordModel = new EncryptPasswordModel();
            encryptPasswordModel.PublicKeyExp = getRsaKeyResponse.PublicKeyExp;
            encryptPasswordModel.PublicKeyMod = getRsaKeyResponse.PublicKeyMod;
            encryptPasswordModel.Password = password;
            return EncryptPassword(encryptPasswordModel);
        }

        private static string EncryptPassword(EncryptPasswordModel encryptPasswordModel)
        {
            var rsa = new RSACryptoServiceProvider();

            var rsaParameters = new RSAParameters
            {
                Exponent = encryptPasswordModel.PublicKeyExp.HexToByte(),
                Modulus = encryptPasswordModel.PublicKeyMod.HexToByte()
            };
            rsa.ImportParameters(rsaParameters);
            var bytePassword = Encoding.ASCII.GetBytes(encryptPasswordModel.Password);
            var encodedPassword = rsa.Encrypt(bytePassword, false);
            return Convert.ToBase64String(encodedPassword);
        }
    }
}
