using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Cinema
{
    public static class UserRegistrationLogic
    {
        public static void Register(string email, string password, string fullName)
        {
            if (!ValidateEmail(email))
            {
                Console.WriteLine("Invalid email format or domain. Email must be '@gmail.com', '@outlook.com', '@gmail.nl', or '@outlook.nl'.");
                return;
            }

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

        public static bool ValidateEmail(string email)
        {

            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            
            return Regex.IsMatch(email, pattern);
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
