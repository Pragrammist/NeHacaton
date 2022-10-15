namespace Web.HasingToken
{
    public interface TokenCryptographer
    {
        public string Encrypt(string token);

        public string Decrypt(string hashedToken);
    }
}
