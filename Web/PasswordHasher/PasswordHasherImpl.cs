using System.Security.Cryptography;
using System.Text;

namespace Web.PasswordHasher
{
    public class PasswordHasherImpl : IPasswordHasher
    {
        readonly string _key;
        public PasswordHasherImpl(IConfiguration configuration)
        {
            _key = configuration.GetSection("Cryptography")["HashKey"];
        }


        public string Hash(string password)
        {
            return HashAlgorithm.Encrypt(password, _key);
        }
    }
}
