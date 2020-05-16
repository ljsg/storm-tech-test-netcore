using System.Security.Cryptography;
using System.Text;

namespace Todo.Services
{
    public static class MD5Hasher
    {
        public static string GetHash(string emailAddress)
        {
            using (var md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.Default.GetBytes(emailAddress.Trim().ToLowerInvariant());
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                var builder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    builder.Append(b.ToString("X2"));
                }
                return builder.ToString().ToLowerInvariant();
            }
        }
    }
}