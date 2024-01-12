using System.Security.Cryptography;
using System.Text;

namespace JWT.Services
{
    public class PasswordHasher
    {
        public (string Hash, string Salt) HashPassword(string password)
        {
            // Генерация случайной соли
            byte[] saltBytes = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            string salt = Convert.ToBase64String(saltBytes);

            // Хеширование пароля с использованием соли
            string hashedPassword = HashPasswordWithSalt(password, salt);

            return (hashedPassword, salt);
        }

        private string HashPasswordWithSalt(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var combinedBytes = Encoding.UTF8.GetBytes(password + salt);
                var hashBytes = sha256.ComputeHash(combinedBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        public bool VerifyPassword(string enteredPassword, string storedHash, string salt)
        {
            // Хеширование введенного пароля с использованием сохраненной соли
            string hashedEnteredPassword = HashPasswordWithSalt(enteredPassword, salt);

            // Сравнение хешей
            return storedHash == hashedEnteredPassword;
        }
    }
}
