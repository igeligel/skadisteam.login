using skadisteam.login.Factories;
using skadisteam.login.Models.Json;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace skadisteam.login.test.Factories
{
    public class PostDataTest
    {
        [Fact]
        public void CreateGetRsaKeyDataTest()
        {
            List<KeyValuePair<string, string>> result =
                PostDataFactory.CreateGetRsaKeyData("usernameOfTheCoolUser");
            var usernameKeyValuePair =
                result.FirstOrDefault(e => e.Key == "username");
            Assert.Equal("username", usernameKeyValuePair.Key);
            Assert.Equal("usernameOfTheCoolUser", usernameKeyValuePair.Value);
        }

        [Fact]
        public void CreateTransferDataAuthTest()
        {
            List<KeyValuePair<string, string>>
                transferParametersKeyValuePairList =
                    PostDataFactory.CreateTransferData(GetDoLoginResponse());
            var authKeyValuePair =
                transferParametersKeyValuePairList.FirstOrDefault(
                    e => e.Key == "auth");
            Assert.Equal("auth", authKeyValuePair.Key);
            Assert.Equal("b9wr1irasauno6rjngp3z2cfwf3qk3an", authKeyValuePair.Value);
        }

        [Fact]
        public void CreateTransferDataRememberLoginTest()
        {
            List<KeyValuePair<string, string>>
                transferParametersKeyValuePairList =
                    PostDataFactory.CreateTransferData(GetDoLoginResponse());
            var rememberLoginKeyValuePair =
                transferParametersKeyValuePairList.FirstOrDefault(
                    e => e.Key == "remember_login");
            Assert.Equal("remember_login", rememberLoginKeyValuePair.Key);
            Assert.True(bool.Parse(rememberLoginKeyValuePair.Value));
        }

        [Fact]
        public void CreateTransferDataSteamIdTest()
        {
            List<KeyValuePair<string, string>>
                transferParametersKeyValuePairList =
                    PostDataFactory.CreateTransferData(GetDoLoginResponse());
            var steamIdKeyValuePair =
                transferParametersKeyValuePairList.FirstOrDefault(
                    e => e.Key == "steamid");
            Assert.Equal("steamid", steamIdKeyValuePair.Key);
            Assert.Equal("76561198028630048", steamIdKeyValuePair.Value);
        }

        [Fact]
        public void CreateTransferDataTokenTest()
        {
            List<KeyValuePair<string, string>>
                transferParametersKeyValuePairList =
                    PostDataFactory.CreateTransferData(GetDoLoginResponse());
            var steamIdKeyValuePair =
                transferParametersKeyValuePairList.FirstOrDefault(
                    e => e.Key == "token");
            Assert.Equal("token", steamIdKeyValuePair.Key);
            Assert.Equal("7ACRVJ9F3D1ETEN9HKFPWSEXZ3X020KRBEZ6HDA7", steamIdKeyValuePair.Value);
        }

        [Fact]
        public void CreateTransferDataTokenSecureTest()
        {
            List<KeyValuePair<string, string>>
                transferParametersKeyValuePairList =
                    PostDataFactory.CreateTransferData(GetDoLoginResponse());
            var steamIdKeyValuePair =
                transferParametersKeyValuePairList.FirstOrDefault(
                    e => e.Key == "token_secure");
            Assert.Equal("token_secure", steamIdKeyValuePair.Key);
            Assert.Equal("O8W437FVTL29F9I31C6H48R0MBNAVIH5ILUMV4FW", steamIdKeyValuePair.Value);
        }

        [Fact]
        public void CreateDoLoginDataUsernameTest()
        {
            var createDoLoginKeyValuePairList = GetDoLoginData();
            var keyValuePair = createDoLoginKeyValuePairList.FirstOrDefault(
                e => e.Key == "username");
            Assert.Equal("username", keyValuePair.Key);
            Assert.Equal("coolUsername01", keyValuePair.Value);
        }

        [Fact]
        public void CreateDoLoginDataEncryptedPasswordTest()
        {
            var createDoLoginKeyValuePairList = GetDoLoginData();
            var keyValuePair = createDoLoginKeyValuePairList.FirstOrDefault(
                e => e.Key == "password");
            Assert.Equal("password", keyValuePair.Key);
            Assert.Equal("o7y4EHhhBs0AaAQtm6LfwdY0SOTcKz57cbqgzrZqSH74PqKOeZ5xDbSfgoqMkOlEUp4ppVNKKRapoGRKecrOtVgcPGhYLpxuw0zcwLjsYQVUYWkOWaVHBnMGJF3L0oghUJl4PEkK0loE5XCfJLhEK3Vg1u8lqrwDJEj16m4sSH4nRiWegcWtLMyZ3hob8xbLHYjORkEZdZI3YlDfrnthXDUQrxzMZPAlADM9UQxUs8CnVTX0igQSAvshkRIaVWAYnBJgBseLWIFCd6bPoMM7Hyngi0oUQhMcVwjjPsXS5axjbjGdwF6Y6gcH4pGbu8FRQ3Tnb4gPhUCZKsS1UOJWmd==", keyValuePair.Value);
        }

        [Fact]
        public void CreateDoLoginDataRsaTimestampTest()
        {
            var createDoLoginKeyValuePairList = GetDoLoginData();
            var keyValuePair = createDoLoginKeyValuePairList.FirstOrDefault(
                e => e.Key == "rsatimestamp");
            Assert.Equal("rsatimestamp", keyValuePair.Key);
            Assert.Equal("464749950000", keyValuePair.Value);
        }

        [Fact]
        public void CreateDoLoginDataTwoFactorCodeTest()
        {
            var createDoLoginKeyValuePairList = GetDoLoginData();
            var keyValuePair = createDoLoginKeyValuePairList.FirstOrDefault(
                e => e.Key == "twofactorcode");
            Assert.Equal("twofactorcode", keyValuePair.Key);
            Assert.Equal("6IPMI", keyValuePair.Value);
        }

        private DoLoginResponse GetDoLoginResponse()
        {
            DoLoginResponse doLoginResponse = new DoLoginResponse();
            TransferParameters transferParameters = new TransferParameters();
            transferParameters.Auth = "b9wr1irasauno6rjngp3z2cfwf3qk3an";
            transferParameters.RememberLogin = true;
            transferParameters.SteamId = "76561198028630048";
            transferParameters.Token =
                "7ACRVJ9F3D1ETEN9HKFPWSEXZ3X020KRBEZ6HDA7";
            transferParameters.TokenSecure =
                "O8W437FVTL29F9I31C6H48R0MBNAVIH5ILUMV4FW";
            doLoginResponse.TransferParameters = transferParameters;
            return doLoginResponse;
        }

        private List<KeyValuePair<string, string>> GetDoLoginData()
        {
            return PostDataFactory.CreateDoLoginData("coolUsername01",
                "o7y4EHhhBs0AaAQtm6LfwdY0SOTcKz57cbqgzrZqSH74PqKOeZ5xDbSfgoqMkOlEUp4ppVNKKRapoGRKecrOtVgcPGhYLpxuw0zcwLjsYQVUYWkOWaVHBnMGJF3L0oghUJl4PEkK0loE5XCfJLhEK3Vg1u8lqrwDJEj16m4sSH4nRiWegcWtLMyZ3hob8xbLHYjORkEZdZI3YlDfrnthXDUQrxzMZPAlADM9UQxUs8CnVTX0igQSAvshkRIaVWAYnBJgBseLWIFCd6bPoMM7Hyngi0oUQhMcVwjjPsXS5axjbjGdwF6Y6gcH4pGbu8FRQ3Tnb4gPhUCZKsS1UOJWmd==",
                "464749950000", "6IPMI");
        }
    }
}
