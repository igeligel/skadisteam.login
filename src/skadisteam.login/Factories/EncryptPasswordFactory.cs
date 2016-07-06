using skadisteam.login.Extensions;
using skadisteam.login.Models;
using skadisteam.login.Models.Json;
using System;
using System.Security.Cryptography;
using System.Text;

namespace skadisteam.login.Factories
{
    internal static class EncryptPasswordFactory
    {
        internal static string Create(GetRsaKeyResponse getRsaKeyResponse, string password)
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
