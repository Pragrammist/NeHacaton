namespace Web.Cryptographer
{
    public interface ICryptographer
    {
        public string Encrypt(string token);
        public string Decrypt(string hashedToken);
    }
}
