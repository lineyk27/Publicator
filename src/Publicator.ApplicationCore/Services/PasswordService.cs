using System.Security.Cryptography;
using System.Text;
using Publicator.ApplicationCore.Contracts;

namespace Publicator.ApplicationCore.Services
{
    class PasswordService : IPasswordService
    {
        public string Encrypt(string text)
        {
            SHA256 algo = SHA256.Create();
            algo.Initialize();
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            byte[] hashed = algo.ComputeHash(bytes);
            //string strhash = Encoding.UTF8.GetString(hashed);

            StringBuilder hashbuilder = new StringBuilder(hashed.Length);
            foreach(var i in hashed)
            {
                hashbuilder.Append(i.ToString("x2"));
            }

            return hashbuilder.ToString();
        }

        public bool IsEqual(string hash1, string hash2)
        {
            return hash1.Equals(hash2);
        }
    }
}
