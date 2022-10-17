namespace Web.HasingToken
{
    public interface ITokenCryptographer
    {
        public string Encrypt(string token);
        public string Decrypt(string hashedToken);
    }
}
