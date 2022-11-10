using System.Text;
using Web.PasswordHasher;

namespace Web.Cryptography
{
    //Код взят из инета, поэтому не знаю как это работает
    public class CryptographerImpl : ICryptographer
    {
        readonly string _key;
         
        public CryptographerImpl(IConfiguration configuration)
        {       
            _key = configuration.GetSection("Cryptography")["HashKey"];  
        }

        
        public string Decrypt(string hashedToken)
        {
            return hashedToken; //HashAlgorithm.Decrypt(hashedToken, _key);
        }

        public string Encrypt(string token)
        {
            return token; //HashAlgorithm.Encrypt(token, _key);
        }

        //string EncryptString(string key, string plainText)
        //{
        //    byte[] iv = new byte[16];
        //    byte[] array;

        //    using (Aes aes = Aes.Create())
        //    {
        //        aes.Key = Encoding.UTF8.GetBytes(key);
        //        aes.IV = iv;

        //        ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        //        using (MemoryStream memoryStream = new MemoryStream())
        //        {
        //            using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
        //            {
        //                using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
        //                {
        //                    streamWriter.Write(plainText);
        //                }

        //                array = memoryStream.ToArray();
        //            }
        //        }
        //    }

        //    return Convert.ToBase64String(array);
        //}

        //string DecryptString(string key, string cipherText)
        //{
        //    byte[] iv = new byte[16];
        //    byte[] buffer = Encoding.UTF8.GetBytes(cipherText);

        //    using (Aes aes = Aes.Create())
        //    {
        //        aes.Key = Encoding.UTF8.GetBytes(key);
        //        aes.IV = iv;
        //        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        //        using (MemoryStream memoryStream = new MemoryStream(buffer))
        //        {
        //            using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
        //            {
        //                using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
        //                {
        //                    return streamReader.ReadToEnd();
        //                }
        //            }
        //        }
        //    }
        //}

    }
}
