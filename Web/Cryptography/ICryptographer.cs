namespace Web.Cryptography
{
    public interface ICryptographer
    {
        public string Encrypt(string token);
        public string Decrypt(string hashedToken);
    }
}
