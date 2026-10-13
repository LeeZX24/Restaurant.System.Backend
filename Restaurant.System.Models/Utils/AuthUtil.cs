using System.Security.Cryptography;

namespace Restaurant.System.Models.Utils
{
    public class AuthUtil
    {
        private static readonly char[] passwordChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_+-=[]{}|;:,.<>?".ToCharArray();
        public static string GenerateRandomPassword(int length = 12)
        {
            if(length < 8) throw new ArgumentException("Password length must be at least 8 characters.");
            return string.Create(length, passwordChars, (chars, alphabet) =>
            {
                for (int i = 0; i < length; i++)
                {
                    chars[i] = alphabet[RandomNumberGenerator.GetInt32(alphabet.Length)];
                }
            });
        }
    }
}
