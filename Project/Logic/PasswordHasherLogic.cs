using System;
using System.Security.Cryptography;
using System.Text;
namespace Cinema;

public static class PasswordHasher
{
    public static string GenerateSalt()
    {
        byte[] bytes = new byte[128 / 8];
        using (var keyGenerator = RandomNumberGenerator.Create())
        {
            keyGenerator.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }

    public static string HashPassword(string password, string salt)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            var saltedPassword = string.Concat(password, salt);
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));

            StringBuilder builder = new StringBuilder();
            foreach (var byteVal in bytes)
            {
                builder.Append(byteVal.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
