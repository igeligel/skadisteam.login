using Newtonsoft.Json;
using skadisteam.login.Factories;
using skadisteam.login.Models;

namespace skadisteam.login.TwoFactor
{
    internal static class OffsetGenerator
    {
        internal static OffsetResponse GetOffset()
        {
            var response = RequestFactory.GetSteamOffset();
            return JsonConvert.DeserializeObject<OffsetResponse>(response.Content.ReadAsStringAsync().Result);
        }

    }
}
