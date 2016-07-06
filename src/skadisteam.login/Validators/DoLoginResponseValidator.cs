using skadisteam.login.Models.Json;

namespace skadisteam.login.Validators
{
    internal static class DoLoginResponseValidator
    {
        internal static bool IsValid(DoLoginResponse doLoginResponse)
        {
            if (doLoginResponse.Success && doLoginResponse.LoginComplete)
            {
                return true;
            }
            return false;
        } 
    }
}
