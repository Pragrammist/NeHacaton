namespace Web.Cryptographer
{
    public interface ApiTokenProvider
    {
        public Task<string> GetToken(string password, string login);
    }
}
