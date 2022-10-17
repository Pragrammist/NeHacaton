using System.Security.Cryptography;
using System.Text;

namespace Web.PasswordHasher
{
    public class PasswordHasherImpl : IPasswordHasher
    {
        public string Hash(string password)
        {
            //: TODO read key from .json config file
            string key = "Something";
            return HashAlgorithm.Encrypt(password, key);
        }
    }
}
