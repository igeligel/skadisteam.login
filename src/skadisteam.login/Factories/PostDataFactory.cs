using skadisteam.login.Constants;
using skadisteam.login.Extensions;
using skadisteam.login.Models.Json;
using System;
using System.Collections.Generic;

namespace skadisteam.login.Factories
{
    internal static class PostDataFactory
    {
        internal static List<KeyValuePair<string, string>> CreateGetRsaKeyData(string username)
        {
            var content = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(PostParameters.Username, username),
                new KeyValuePair<string, string>(PostParameters.DoNotCache, DateTime.UtcNow.ToUnixMilliSecondTime().ToString())
            };
            return content;
        }

        internal static List<KeyValuePair<string, string>> CreateTransferData(DoLoginResponse doLoginResponse)
        {
            var content = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(PostParameters.Auth, doLoginResponse.TransferParameters.Auth),
                new KeyValuePair<string, string>(PostParameters.RememberLogin, doLoginResponse.TransferParameters.RememberLogin.ToString()),
                new KeyValuePair<string, string>(PostParameters.SteamId, doLoginResponse.TransferParameters.SteamId),
                new KeyValuePair<string, string>(PostParameters.Token, doLoginResponse.TransferParameters.Token),
                new KeyValuePair<string, string>(PostParameters.TokenSecure, doLoginResponse.TransferParameters.TokenSecure),
            };
            return content;
        }

        internal static List<KeyValuePair<string, string>> CreateDoLoginData(string username, string encryptedPassword, string rsaTimestamp, string twoFactorCode)
        {
            var content = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(PostParameters.Username, username),
                new KeyValuePair<string, string>(PostParameters.Password, encryptedPassword),
                new KeyValuePair<string, string>(PostParameters.CaptchaGid, "-1"),
                new KeyValuePair<string, string>(PostParameters.CaptchaText, ""),
                new KeyValuePair<string, string>(PostParameters.RememberLogin, "true"),
                new KeyValuePair<string, string>(PostParameters.LoginFriendlyName, ""),
                new KeyValuePair<string, string>(PostParameters.EmailAuth, ""),
                new KeyValuePair<string, string>(PostParameters.EmailSteamId, ""),
                new KeyValuePair<string, string>(PostParameters.RsaTimestamp, Uri.EscapeDataString(rsaTimestamp)),
                new KeyValuePair<string, string>(PostParameters.DoNotCache, DateTime.UtcNow.ToUnixMilliSecondTime().ToString()),
                new KeyValuePair<string, string>(PostParameters.TwoFactorCode, twoFactorCode)
            };
            return content;
        }
    }
}
