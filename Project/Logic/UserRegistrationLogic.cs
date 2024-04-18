using System;
using System.Linq;

namespace Cinema
{
    public static class UserRegistrationLogic
    {
        public static void Register(string email, string password, string fullName)
        {
            if (!ValidatePassword(password))
            {
                Console.WriteLine("Password does not meet the security requirements.");
                return;
            }

            UserRepository userRepository = new UserRepository();
            if (userRepository.UserExists(email))
            {
                Console.WriteLine("A user with this email already exists.");
                return;
            }

            var salt = PasswordHasher.GenerateSalt();
            var hashedPassword = PasswordHasher.HashPassword(password, salt);

            // Include isActive: false when creating a new user
            AccountModel newUser = new AccountModel(email, hashedPassword, salt, fullName, isAdmin: false, isActive: false);
            userRepository.AddUser(newUser);

            Console.WriteLine("Registration successful. Welcome, " + fullName);
        }

        public static bool ValidatePassword(string password)
        {
            if (password.Length < 8)
            {
                Console.WriteLine("Password must be at least 8 characters long.");
                return false;
            }
            if (!password.Any(char.IsUpper))
            {
                Console.WriteLine("Password must contain at least one uppercase letter.");
                return false;
            }
            if (!password.Any(char.IsDigit))
            {
                Console.WriteLine("Password must contain at least one number.");
                return false;
            }
            if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                Console.WriteLine("Password must contain at least one symbol.");
                return false;
            }
            return true;
        }
    }
}
