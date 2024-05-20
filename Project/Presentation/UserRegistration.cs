using System;

namespace Cinema
{
    static class UserRegistration
    {
        public static void Start()
        {
            DisplayHeader.RegistrationHeader();
            Console.WriteLine("\n---------------------------------------------------------------------------\n");
            Console.WriteLine("Welcome to the registration page!");
            Console.WriteLine("Please enter your email address\n");

            Console.Write(">>> ");
            string email = Console.ReadLine()!;

            Console.Clear();
            DisplayHeader.RegistrationHeader();
            Console.WriteLine("\n---------------------------------------------------------------------------\n");

            Console.WriteLine("Password must be/ contain:\nAt least 8 characters long.\nAt least one uppercase letter.\nAt least one number.\nAt least one symbol.");
            string password = GetPassword("Please enter your password:");
            string confirmPassword = GetPassword("Please re-enter your password for confirmation:");

            if (password != confirmPassword)
            {
                Console.WriteLine("Passwords do not match. Please try registering again.");
                return;
            }

            Console.WriteLine("Please enter your full name:");
            string fullName = Console.ReadLine()!;

            // Proceed with the registration
            UserRegistrationLogic.Register(email, password, fullName);
        }

        private static string GetPassword(string prompt)
        {
            Console.WriteLine($"{prompt}\n");
            Console.Write(">>> ");
            string password = "";
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password[..^1];
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
            }
            return password;
        }
    }
}
