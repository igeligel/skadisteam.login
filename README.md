# skadisteam.login

A library to login into [steamcommunity.com](http://steamcommunity.com/) via a .net core wrapper.
This will not need an api key because it is using the methods which steam is providing via the web.

# TODO's

| Issue         | Description                                    |
| ------------- | ---------------------------------------------- |
| #1            | Add time aligner to two factor code generator. |

# Dependencies

- Newtonsoft.Json 9.0.1

# Installation

1. Make sure you have .net core installed. Head over to [dot.net](http://dot.net) to install .net core.

## Via a nuget package

> Work in progress

## Via class library

1. Clone the repo by using git or download it as .zip.
2. In your new project add the project to the global.json file like this:

  ```json
  {
     "projects": [ "src", "test", "../skadisteamlogin/src" ],
     "sdk": {
       "version": "1.0.0-preview2-003121"
     }
  }
  ```

  ```json
  "../skadisteamlogin/src"
  ```
  is the path to the project.

3. Add the reference to your project.json like this:
  ```json
  {
    "dependencies": {
      "Microsoft.NETCore.App": {
        "type": "platform",
        "version": "1.0.0"
      },
      "skadisteam.login": "1.0.0-*"
    }
  }
  ``` 

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

You can also use skadi configuration for adding extra features.
```csharp
SkadiLoginConfiguration skadiLoginConfiguration = new SkadiLoginConfiguration
{
    StopOnError = false,
    WaitTimeEachError = 20
};
```

This needs to be appended to the login instance. This is done by:

```csharp
var skadiLogin = new SkadiLogin(skadiLoginConfiguration);
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
| SkadiLoginCookies | CookieContainer | CookieContainer which contains the cookies of the login | {System.Net.CookieContainer} |
| SkadiLoginError | SkadiLoginError | If an error occurs this will be an instance of SkadiLoginError. See the documentation of it for more information | null |
| SteamCommunityId | long | Steam Community Id of the account which logged in | 76561198028630048 |
| SteamCountry | string | Country provided by Steam | DE... |
| SteamLanguage | string | Language which is set. Default is english. | "english" |
| SteamLogin | string | Steam login cookie value. | "76561198028630048AWd12km8d_dwaknN21..." |
| SteamRememberLogin | string | Steam's value of the cookie to remember login | "76561198028630048AWd12km8d_dwaknN21..." |

# License
[MIT License](https://github.com/igeligel/skadisteam.login/blob/master/LICENSE)

# How to grab documentation

-/-

# Contributing

## Commits
For commits i am using [this style](https://github.com/igeligel/contributing-template/blob/master/commits.md). You should also use this style when you are creating pull requests.

## Csharp
For general language advice i suggest the [official style guideline](https://msdn.microsoft.com/en-us/library/ff926074.aspx). Written down in markdown syntax [here](https://github.com/igeligel/contributing-template/blob/master/code-style/csharp.md).

# Authors

- [igeligel](https://github.com/igeligel)

# Contact information

[![Steam](https://raw.githubusercontent.com/encharm/Font-Awesome-SVG-PNG/master/black/png/16/steam-square.png "Steam Account") Steam](http://steamcommunity.com/profiles/76561198028630048/)

[![Discord](http://i.imgur.com/wlwOQpl.png "Discord") Discord](https://discord.gg/011jg2foytc2XogS6)

[![Twitter](https://raw.githubusercontent.com/encharm/Font-Awesome-SVG-PNG/master/black/png/16/twitter.png "Twitter") Twitter](https://twitter.com/kevinpeters_)

# History

| Date          | Version       | Description          |
| ------------- | ------------- | -------------------- |
| 06/30/16      | -/-           | Start of the project |