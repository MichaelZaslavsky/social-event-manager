using System.Security.Cryptography;
using System.Text;

namespace SocialEventManager.Shared.Helpers
{
    public static class StringHelpers
    {
        public static string GenerateRandomValue(int length = 50)
        {
            char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*".ToCharArray();
            byte[] data = new byte[1];

            using var crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[length];
            crypto.GetNonZeroBytes(data);

            var builder = new StringBuilder(length);
            foreach (byte b in data)
            {
                builder.Append(chars[b % chars.Length]);
            }

            return builder.ToString();
        }
    }
}
