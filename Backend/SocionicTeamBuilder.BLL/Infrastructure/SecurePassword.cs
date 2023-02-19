using System.Security.Cryptography;
using System.Text;

namespace SocionicTeamBuilder.BLL.Infrastructure
{
    public static class SecurePassword
    {
        public static string GetHashString(string password)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] byteHash = MD5.HashData(bytes);

            string hash = string.Empty;
            foreach (byte b in byteHash)
            {
                hash += string.Format("{0:x2}", b);
            }

            return hash;
        }
    }
}
