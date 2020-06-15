using System.Security.Cryptography;
using System.Text;

namespace Projeto.CrossCutting.Cryptography
{
    public class MD5Encrypt
    {
        public static string GenerateHash(string value)
        {
            var hash = new MD5CryptoServiceProvider()
                .ComputeHash(Encoding.UTF8.GetBytes(value));

            var result = string.Empty;

            foreach (var item in hash)
            {
                result += item.ToString("X2");
            }
            return result;
        }
    }
}
