using skadi_steam_login.TwoFactor;

namespace skadi_steam_login.Factories
{
    public static class TwoFactorCodeFactory
    {
        public static string Create(string sharedSecret)
        {
            var twoFactorGenerator = new SteamTwoFactorGenerator
            {
                SharedSecret = sharedSecret
            };
            return twoFactorGenerator.GenerateSteamGuardCodeForTime();
        }
    }
}
