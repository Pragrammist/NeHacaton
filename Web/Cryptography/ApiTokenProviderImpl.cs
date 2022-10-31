using HendInRentApi;

namespace Web.Cryptography
{
    public class ApiTokenProviderImpl : ApiTokenProvider
    {
        HIRALogin<OutputHIRAAuthTokenDto, InputHIRALoginUserDto> _hIRALogin;
        ICryptographer _cryptographer;
        public ApiTokenProviderImpl(HIRALogin<OutputHIRAAuthTokenDto, InputHIRALoginUserDto> hIRALogin, ICryptographer cryptographer)
        {
            _hIRALogin = hIRALogin;
            _cryptographer = cryptographer;
        }

        async Task<string> GetTokenFromDecryptedPassword(string pass, string login)
        {
            var objToken = await _hIRALogin.Login(new InputHIRALoginUserDto { Login = login, Password = pass });
            var tokenResult = objToken.AccessToken;
            return tokenResult;
        }
        public async Task<string> GetToken(string password, string login) => await GetTokenFromDecryptedPassword(password, login);


        public async Task<string> GetTokenFrom(string encryptedPassword, string login)
        {
            var pass = _cryptographer.Decrypt(encryptedPassword);
            return await GetTokenFromDecryptedPassword(pass, login);
        }
    }
}
