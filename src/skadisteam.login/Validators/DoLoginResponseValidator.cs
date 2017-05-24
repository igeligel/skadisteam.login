using skadisteam.login.Models.Json;

namespace skadisteam.login.Validators
{
    internal static class DoLoginResponseValidator
    {
        internal static bool IsValid(DoLoginResponse doLoginResponse)
        {
            return doLoginResponse.Success && doLoginResponse.LoginComplete;
        } 
    }
}
