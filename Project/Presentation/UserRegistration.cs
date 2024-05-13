using System;

namespace Cinema
{
    static class UserRegistration
    {
        public static void Start()
        {
            Console.WriteLine("Welcome to the registration page.");

            string email;
            while (true)
            {
                Console.WriteLine("Please enter your email address:");
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("Registration has been cancelled.");
                    return; // Early exit if Esc is pressed
                }
                email = Console.ReadLine();
                if (UserRegistrationLogic.ValidateEmail(email))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid email. Please ensure the email contains '@gmail.com', '@outlook.com', '@gmail.nl', or '@outownline.nl'.");
                }
            }

            string password = GetValidPassword("Please enter your password:\nMust at least contain 8 characters, one capital letter, one symbol, and one number:\n");
            if (password == null) // Check if the user pressed Esc to quit during password input
            {
                Console.WriteLine("Registration cancelled.");
                return;
            }

            string confirmPassword = GetPassword("Please re-enter your password for confirmation:");
            if (confirmPassword == null) // Check if the user pressed Esc to quit during password confirmation
            {
                Console.WriteLine("Registration cancelled.");
                return;
            }

            if (password != confirmPassword)
            {
                Console.WriteLine("Passwords do not match. Please try registering again.");
                return;
            }

            Console.WriteLine("Please enter your full names:");
            if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
            {
                Console.WriteLine("Registration has been cancelled.");
                return; // Early exit if Esc is pressed
            }
            string fullName = Console.ReadLine();

            // Proceed with the registration
            UserRegistrationLogic.Register(email, password, fullName);
        }

        private static string GetPassword(string prompt)
        {
            Console.WriteLine(prompt);
            string password = "";
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    if (password.Length > 0)
                    {
                        Console.WriteLine();
                        break;
                    }
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    return null; // Return null if Esc is pressed to indicate cancellation
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

        private static string GetValidPassword(string prompt)
        {
            string password = "";
            do
            {
                password = GetPassword(prompt);
                if (password == null) return null; // Return null if Esc is pressed to indicate cancellation

                if (!UserRegistrationLogic.ValidatePassword(password))
                {
                    Console.WriteLine("Password does not meet the requirements. Please try again.");
                }
            }
            while (!UserRegistrationLogic.ValidatePassword(password));

            return password;
        }
    }
}
