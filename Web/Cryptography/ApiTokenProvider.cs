namespace Web.Cryptography
{
    //todo move in another namespace and folder
    public interface ApiTokenProvider
    {
        public Task<string> GetToken(string password, string login);

        public Task<string> GetTokenFrom(string encryptedPassword, string login);
    }
}
