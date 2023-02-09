using System.Security.Cryptography;
using System.Text;
using TransactionSyncAPI.Services.Intarfaces.InternalServices;

namespace TransactionSyncAPI.Services.Realization.InternalServices
{
    public class PasswordHasher : IPasswordHasher
    {
        public string ComputeHash(string password, string salt)
        {
            using var sha256 = SHA256.Create();
            var passwordWithSalt = $"{password}{salt}";
            var bytePassword = Encoding.UTF8.GetBytes(passwordWithSalt);
            var byteHash = sha256.ComputeHash(bytePassword);
            string hashString = Encoding.UTF8.GetString(byteHash);

            return hashString;
        }

        public string GenerateSalt()
        {
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            var byteSalt = new byte[16];
            randomNumberGenerator.GetBytes(byteSalt);
            string salt = Convert.ToBase64String(byteSalt);

            return salt;
        }
    }
}
