using System.Security.Cryptography;
using System.Text;

namespace Web.PasswordHasher
{
    public class PasswordHasherImpl : IPasswordHasher
    {
        readonly Encoding encoding = Encoding.UTF8;
        public string Hash(string password)
        {
            var passwordBytes = encoding.GetBytes(password);

            var hashBytes = MD5.HashData(passwordBytes);

            var hash = encoding.GetString(hashBytes);

            return hash;
        }
    }
}
