using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using skadisteam.login.Constants;
using skadisteam.login.Http.Headers;
using skadisteam.login.Models;
using skadisteam.login.Models.Json;
using skadisteam.login.Factories;
using skadisteam.login.Http;
using skadisteam.login.Validators;
using skadisteam.shared.Models;

namespace skadisteam.login
{
    /// <summary>
    /// Class which has the required methods to make 
    /// a login into the steamcommunity.
    /// </summary>
    public class SkadiLogin
    {
        private RequestFactory _requestFactory;
        private SkadiLoginConfiguration _skadiLoginConfiguration;

        /// <summary>
        /// Standard constructor. When used this the default settings are used.
        /// These are:
        /// <list type="table">
        ///     <listheader>
        ///         <term>Property</term>
        ///         <description>Value</description>
        ///     </listheader>
        ///     <item>
        ///         <term>StopOnError</term>
        ///         <description>true</description>
        ///     </item>
        ///     <item>
        ///         <term>WaitTimeEachError</term>
        ///         <description>5</description>
        ///     </item>
        /// </list>
        /// For more information lookup: <see cref="SkadiLoginConfiguration"/>.
        /// </summary>
        public SkadiLogin()
        {
            _requestFactory = new RequestFactory();
        }

        /// <summary>
        /// Constructor which takes a <see cref="SkadiLoginConfiguration"/>.
        /// You can edit the default behaviour there.
        /// </summary>
        /// <param name="skadiLoginConfiguration">
        /// Login configuration which should be used. 
        /// See: <see cref="SkadiLoginConfiguration"/>.
        /// </param>
        public SkadiLogin(SkadiLoginConfiguration skadiLoginConfiguration)
        {
            _requestFactory = new RequestFactory();
            _skadiLoginConfiguration = skadiLoginConfiguration;
        }

        /// <summary>
        /// Execute the login. This will take the configuration into consideration
        /// which can be given as parameter in the constructor.
        /// </summary>
        /// <param name="skadiLoginData">
        /// Date of the steam login. See <see cref="SkadiLoginData"/>.
        /// </param>
        /// <returns>
        /// It will return a response with login data.
        /// For more information lookup <see cref="SkadiLoginResponse"/>.
        /// </returns>
        public SkadiLoginResponse Execute(SkadiLoginData skadiLoginData)
        {
            GetSession();
            if (_skadiLoginConfiguration != null &&
                !_skadiLoginConfiguration.StopOnError)
            {
                return ExecuteUntilLogin(skadiLoginData);
            }
            var rsaKey = GetRsaKey(skadiLoginData.Username);
            var doLoginResponse = DoLogin(rsaKey, skadiLoginData.Username,
                skadiLoginData.Password, skadiLoginData.SharedSecret);
            
            if (!DoLoginResponseValidator.IsValid(doLoginResponse))
            {
                SkadiLoginResponse skadiLoginResponse = new SkadiLoginResponse();
                skadiLoginResponse.SkadiLoginError =
                    SkadiLoginErrorFactory.Create(doLoginResponse);
                return skadiLoginResponse;
            }
            Transfer(doLoginResponse);
            return SetSession();
        }

        private SkadiLoginResponse ExecuteUntilLogin(SkadiLoginData skadiLoginData)
        {
            GetSession();
            GetRsaKeyResponse rsaKey = new GetRsaKeyResponse();
            DoLoginResponse doLoginResponse = new DoLoginResponse();
            var doLoginSuccessful = false;

            do
            {
                try
                {
                    rsaKey = GetRsaKey(skadiLoginData.Username);
                    doLoginResponse = DoLogin(rsaKey, skadiLoginData.Username,
                        skadiLoginData.Password, skadiLoginData.SharedSecret);
                    if (!DoLoginResponseValidator.IsValid(doLoginResponse))
                    {
                        if (doLoginResponse.CaptchaNeeded)
                        {
                            // TODO: Get exact time for cooldown of captcha!
                            Task.Delay(TimeSpan.FromMinutes(25));
                        }
                        rsaKey = null;
                        doLoginResponse = null;
                    }
                    else
                    {
                        doLoginSuccessful = true;
                    }
                }
                catch (Exception)
                {
                    Task.Delay(
                        TimeSpan.FromSeconds(
                            _skadiLoginConfiguration.WaitTimeEachError)).Wait();
                }
            }
            while (doLoginSuccessful == false);
            
            bool errorInTransfer = false;
            do
            {
                try
                {
                    Transfer(doLoginResponse);
                }
                catch (Exception)
                {
                    errorInTransfer = true;
                    Task.Delay(TimeSpan.FromSeconds(5)).Wait();
                }
                
            } while (errorInTransfer);

            SkadiLoginResponse skadiLoginResponse = null;
            do
            {
                try
                {
                    skadiLoginResponse = SetSession();
                }
                catch (Exception)
                {
                    Task.Delay(TimeSpan.FromSeconds(5)).Wait();
                }

            } while (skadiLoginResponse == null);
            
            return skadiLoginResponse;
        }

        private void GetSession()
        {
            _requestFactory.CreateSession();
        }

        private GetRsaKeyResponse GetRsaKey(string username)
        {
            var postContent = PostDataFactory.CreateGetRsaKeyData(username);
            GetRsaKeyResponse getRsaKeyResponse = null;

            var response = _requestFactory.Create(HttpMethod.POST,
                Uris.SteamCommunitySecureBase,
                SteamCommunityEndpoints.GetRsaKey, Accept.All,
                HttpHeaderValues.AcceptLanguageOne, false, true, true, true,
                false, postContent);
            string responseContent = response.Content.ReadAsStringAsync().Result;
            getRsaKeyResponse =
                JsonConvert.DeserializeObject<GetRsaKeyResponse>(responseContent);
            return getRsaKeyResponse;
        }

        private DoLoginResponse DoLogin(GetRsaKeyResponse getRsaKeyResponse,
            string username, string password, string sharedSecret)
        {
            DoLoginResponse doLoginResponse = null;
            var encryptedPassword = EncryptPasswordFactory.Create(getRsaKeyResponse, password);

            var content = PostDataFactory.CreateDoLoginData(username,
                encryptedPassword, getRsaKeyResponse.Timestamp,
                TwoFactorCodeFactory.Create(sharedSecret));

            var response = _requestFactory.Create(HttpMethod.POST,
                Uris.SteamCommunitySecureBase,
                SteamCommunityEndpoints.DoLogin, Accept.All,
                HttpHeaderValues.AcceptLanguageOne, false, true, true, true,
                false, content);

            string responseContent = response.Content.ReadAsStringAsync().Result;
            doLoginResponse =
                JsonConvert.DeserializeObject<DoLoginResponse>(responseContent);
            return doLoginResponse;
        }
        
        private void Transfer(DoLoginResponse doLoginResponse)
        {
            var content = PostDataFactory.CreateTransferData(doLoginResponse);
            _requestFactory.Create(HttpMethod.POST,
                Uris.HelpSteampoweredSecureBase,
                HelpSteampoweredEndpoints.TransferLogin,
                Accept.Html, HttpHeaderValues.AcceptLanguageTwo, true, true,
                true, false, true, content);
        }

        private SkadiLoginResponse SetSession()
        {
            var response = _requestFactory.Create(HttpMethod.GET,
                Uris.SteamCommunityBase, SteamCommunityEndpoints.Home,
                Accept.Html, HttpHeaderValues.AcceptLanguageTwo, true, false,
                false, false, false, null);
            return SkadiLoginResponseFactory.Create(response, _requestFactory.GetCookieContainer());
        }
    }
}
