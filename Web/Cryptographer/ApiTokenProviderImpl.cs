using HendInRentApi;

namespace Web.Cryptographer
{
    public class ApiTokenProviderImpl : ApiTokenProvider
    {
        HIRALogin<OutputHIRAAuthTokenDto, InputHIRALoginUserDto> _hIRALogin;
        public ApiTokenProviderImpl(HIRALogin<OutputHIRAAuthTokenDto, InputHIRALoginUserDto> hIRALogin)
        {
            _hIRALogin = hIRALogin;
        }


        public async Task<string> GetToken(string password, string login)
        {
            var objToken = await _hIRALogin.Login(new InputHIRALoginUserDto { Login = login, Password = password});
            var tokenResult = objToken.AccessToken;
            return tokenResult;
        }
    }
}
