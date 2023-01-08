using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;

namespace Cw9.Services
{
    public class PasswordEncrypter
    {
        public static string Encrypt(string pass, string salt)
        {
            var valueBytes = KeyDerivation.Pbkdf2(
                password: pass,
                salt: Encoding.UTF8.GetBytes(salt),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 1000,
                numBytesRequested: 256 / 8);

            return Convert.ToBase64String(valueBytes);
        }

        public static bool ValidatePassworEncryption(string pass, string salt, string encr)
        {
            var r = Encrypt(pass, salt);
            return r.Equals(encr);
        }

        public static string CreateSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }
    }
}
