using skadisteam.login.TwoFactor;

namespace skadisteam.login.Factories
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
