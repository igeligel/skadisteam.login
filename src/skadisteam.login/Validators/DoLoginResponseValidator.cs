using skadisteam.login.Models.Json;
using skadi_steam_login.Models;

namespace skadi_steam_login.Validators
{
    public static class DoLoginResponseValidator
    {
        public static bool IsValid(DoLoginResponse doLoginResponse)
        {
            if (doLoginResponse.Success && doLoginResponse.LoginComplete)
            {
                return true;
            }
            return false;
        } 
    }
}
