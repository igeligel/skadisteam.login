using skadisteam.login.Models.Json;
using skadisteam.login.Models;

namespace skadisteam.login.Validators
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
