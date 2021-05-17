using System;
using System.Security.Cryptography;
using System.Text;

namespace SocialEventManager.Shared.Helpers
{
    public static class RandomGeneratorHelpers
    {
        private static readonly RNGCryptoServiceProvider CryptoServiceProvider = new ();

        public static string GenerateRandomValue(int length = 50)
        {
            char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*".ToCharArray();
            var data = new byte[1];

            CryptoServiceProvider.GetNonZeroBytes(data);
            data = new byte[length];
            CryptoServiceProvider.GetNonZeroBytes(data);

            var builder = new StringBuilder(length);
            foreach (byte b in data)
            {
                builder.Append(chars[b % chars.Length]);
            }

            return builder.ToString();
        }

        public static int NextInt32()
        {
            byte[] randomBytes = GenerateRandomBytes(sizeof(int));
            return BitConverter.ToInt32(randomBytes, 0);
        }

        #region Private Methods

        private static byte[] GenerateRandomBytes(int bytesNumber)
        {
            var buffer = new byte[bytesNumber];
            CryptoServiceProvider.GetBytes(buffer);

            return buffer;
        }

        #endregion Private Methods
    }
}
