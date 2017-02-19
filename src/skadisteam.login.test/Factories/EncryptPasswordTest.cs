using skadisteam.login.Factories;
using skadisteam.login.Models.Json;
using Xunit;

namespace skadisteam.login.test.Factories
{
    public class EncryptPasswordTest
    {
        [Fact]
        public void Format()
        {
            const string password = "coolPassword164";
            GetRsaKeyResponse getRsaKeyResponse = new GetRsaKeyResponse();
            getRsaKeyResponse.PublicKeyMod =
                "b57ab169e7cff1db44eba0da7e1effab9d62ba371a4c144d5371cc78e80d1c9588c8f9036ff4a36b093b80f555a6c8fb4d3a0ee862581a4ee3d4165094269e39477497873e0740e0b6ad602f10d0aa4afe4acb211ac43dbe2de78f69a400124b78f0c4994f88447a0890fe058b3124a4c87672e5604ea07314ae44603cec455edd502da0bada4de46dec9b1094cbd1412e4326e64be222ef95a10e914e00e957762a938bee96bbf649bec12e68fe734206d35c79443095cb300f7d230d20739d0e040eeb492372cc1ed09f5327c2f17bae46e85f52ec1d33bcf37079345dacf131a02db9b387fa1a75b001561a4e1cd1ef3f975b6dcf0f6a62e2f6976a37daa9";
            getRsaKeyResponse.PublicKeyExp = "010001";
            var encryptedPassword = EncryptPasswordFactory.Create(getRsaKeyResponse, password);
            Assert.Contains("==", encryptedPassword);
        }

        [Fact]
        public void Length()
        {
            const string password = "coolPassword164";
            GetRsaKeyResponse getRsaKeyResponse = new GetRsaKeyResponse();
            getRsaKeyResponse.PublicKeyMod =
               "b57ab169e7cff1db44eba0da7e1effab9d62ba371a4c144d5371cc78e80d1c9588c8f9036ff4a36b093b80f555a6c8fb4d3a0ee862581a4ee3d4165094269e39477497873e0740e0b6ad602f10d0aa4afe4acb211ac43dbe2de78f69a400124b78f0c4994f88447a0890fe058b3124a4c87672e5604ea07314ae44603cec455edd502da0bada4de46dec9b1094cbd1412e4326e64be222ef95a10e914e00e957762a938bee96bbf649bec12e68fe734206d35c79443095cb300f7d230d20739d0e040eeb492372cc1ed09f5327c2f17bae46e85f52ec1d33bcf37079345dacf131a02db9b387fa1a75b001561a4e1cd1ef3f975b6dcf0f6a62e2f6976a37daa9";
            getRsaKeyResponse.PublicKeyExp = "010001";
            var encryptedPassword = EncryptPasswordFactory.Create(getRsaKeyResponse, password);
            Assert.True(password.Length <= encryptedPassword.Length);
        }
    }
}
