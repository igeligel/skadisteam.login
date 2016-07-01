# skadisteam.login

A library to login into [steamcommunity.com](http://steamcommunity.com/) via a .net core wrapper.
This will not need an api key because it is using the methods which steam is providing via the web.

# Dependencies

- Newtonsoft.Json 9.0.1

# How to use

At first you need to create the login data. The library will provide a model for this.
You can create an instance like this:
```csharp
var skadiLoginData = new SkadiLoginData {
    Username = "username",
    Password = "password",
    SharedSecret = "sharedSecret"
};
```

After this we need to instantiate the login by:
```csharp
var skadiLogin = new SkadiLogin();
```

After instantiating you can execute the login by applying the data to a method.
```csharp
var skadiLoginResponse = skadiLogin.Execute(skadiLoginData);
```

The response will have several informations and the cookie container of the login.
The object will have the following properties:

| Parameter     | Type | Description   | Example  |
| ------------- | ---- | ------------- | -------- |
| SessionId | string | Id of the session | "wdIaDW21adsAh" |
| SkadiLoginCookies | CookieContainer | CookieContainer which contains the cookies of the login | -/- |
| SteamCommunityId | long | Steam Community Id of the account which logged in | 76561198028630048 |
| SteamCountry | string | Country provided by Steam | DE... |
| SteamLanguage | string | Language which is set. Default is english. | "english" |
| SteamLogin | string | Steam login cookie value. | "76561198028630048AWd12km8d_dwaknN21..." |
| SteamRememberLogin | string | Steam's value of the cookie to remember login | "76561198028630048AWd12km8d_dwaknN21..." |

# License

# How to grab documentation

# Install

# Authors

# Contributing

# Contact information

# History