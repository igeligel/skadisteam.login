using skadisteam.login.TwoFactor;

namespace skadisteam.login.Factories
{
    internal static class TwoFactorCodeFactory
    {
        internal static string Create(string sharedSecret)
        {
            var twoFactorGenerator = new SteamTwoFactorGenerator
            {
                SharedSecret = sharedSecret
            };
            return twoFactorGenerator.GenerateSteamGuardCodeForTime();
        }
    }
}
