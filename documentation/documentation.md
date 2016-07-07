# skadisteam.login


## Constants.HelpSteampoweredEndpoints

Class which contains constants for interacting with the help.steampowered endpoint.


## Extensions.CharExtensions

Class to extend functionality for character type. We need this for creating special encrypting mechanics.


### M:skadisteam.login.GetHexVal(hex)

Get the Hex Value out of char.

| Name | Description |
| ---- | ----------- |
| hex | *System.Char*<br> Char to input.  |


#### Returns

 An int.


## Extensions.CookieContainerExtensions

Class to extend functionality of the CookieContainer. We need this to add special cookies to our CookieContainer.


### M:skadisteam.login.AddEnglishSteamLanguage(cookieContainer)

Method to add the english steam language to the CookieContainer. It is needed to set a standard language for the package.

| Name | Description |
| ---- | ----------- |
| cookieContainer | *System.Net.CookieContainer*<br> CookieContainer which the cookie should be added to.  |

### M:skadisteam.login.CreateLanguageCookie(uri)

Will just create the cookie containing the steam language parameters.

| Name | Description |
| ---- | ----------- |
| uri | *System.Uri*<br> Uri referring the urlwhich should be used for the cookie.  |


#### Returns

The Cookie instance containing the required information.


## Extensions.DateTimeExtensions

Class to extend functionality of the DateTime type.


### M:skadisteam.login.ToUnixMilliSecondTime(dateTimeInstance)

Method to convert a date time object to the unix timestamp. It is the millisecond type of the timestamp.

| Name | Description |
| ---- | ----------- |
| dateTimeInstance | *System.DateTime*<br>Instance of the DateTime type. |


#### Returns

 A long containing the unix timestamp in millisecond format.


## Extensions.StringExtensions

Class to extend functionality of the normal string methods.


### M:skadisteam.login.HexToByte(hex)

Hex to Byte method.

| Name | Description |
| ---- | ----------- |
| hex | *System.String*<br> Hex value.  |


#### Returns

 Hex Value as Byte[].


## Models.Json.ErrorType

Enumeration to describe errors of the login.


## Models.SkadiLoginError

Error which is probably given by the login. It will contain several information about why the login failed.


### .CaptchaGid

Contains the captcha id if a captcha is needed. If no captcha is needed it will be -1.


### .CaptchaNeeded

Value which is describing if a captcha is needed.


### .Message

Message given by steam why the login failed.


### .Type

Type of the error. For further information lookup .


## Models.SkadiLoginResponse

Response which is given of the response. This is used for interacting with other services.


### .SessionId

Id of your session.


### .SkadiLoginCookies

The CookieContainer which is used for the login.


### .SkadiLoginError

Error instance which is given if the login fails. See for more information.


### .SteamCommunityId

The steam community id of the account which logged in.


### .SteamCountry

Country which is given by steam.


### .SteamLanguage

Steam language which is set. Should be english.


### .SteamLogin

Steam login value.


### .SteamRememberLogin

Steam remember login value.


## SkadiLogin

Class which has the required methods to make a login into the steamcommunity.


### M:skadisteam.login.#ctor

Standard constructor. When used this the default settings are used. These are: PropertyValueStopOnErrortrueWaitTimeEachError5 For more information lookup: .


### M:skadisteam.login.#ctor(skadiLoginConfiguration)

Constructor which takes a . You can edit the default behaviour there.

| Name | Description |
| ---- | ----------- |
| skadiLoginConfiguration | *skadisteam.login.Models.SkadiLoginConfiguration*<br> Login configuration which should be used. See: .  |

### M:skadisteam.login.Execute(skadiLoginData)

Execute the login. This will take the configuration into consideration which can be given as parameter in the constructor.

| Name | Description |
| ---- | ----------- |
| skadiLoginData | *skadisteam.login.Models.SkadiLoginData*<br> Date of the steam login. See .  |


#### Returns

 It will return a response with login data. For more information lookup .


## TwoFactor.SteamTwoFactorGenerator

Class to enable two factor authorization for the login process on backpack.tf.


### M:skadisteam.login.GenerateSteamGuardCodeForTime

Generate Steam Guard Code for a specific time. Therefore you need the shared secret attacked to an instance of this file.


#### Returns

The string which is five characters long which you need to authenticate on steam.


### F:skadisteam.login.SharedSecret

String which is the shared secret. This will be provided if you add an authenticator. You will get your shared secret here: https://api.steampowered.com/ITwoFactorService/AddAuthenticator/v0001. This is the json response deserialized to an c# object. public class Response { public string shared_secret { get; set; } public string serial_number { get; set; } public string revocation_code { get; set; } public string uri { get; set; } public string server_time { get; set; } public string account_name { get; set; } public string token_gid { get; set; } public string identity_secret { get; set; } public string secret_1 { get; set; } public int status { get; set; } } public class RootObject { public Response response { get; set; } } You will just need the shared_secret.


### F:skadisteam.login.SteamGuardCodeTranslations

byte to do the Steam Guard Code translation.