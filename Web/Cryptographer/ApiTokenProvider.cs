namespace Web.Cryptographer
{
    //todo move in another namespace and folder
    public interface ApiTokenProvider
    {
        public Task<string> GetToken(string password, string login);
    }
}
