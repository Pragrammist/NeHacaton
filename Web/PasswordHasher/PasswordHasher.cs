namespace Web.PasswordHasher
{
    public interface IPasswordHasher
    {
        public string Hash(string password);
    }
}
