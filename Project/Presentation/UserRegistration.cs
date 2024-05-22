using System;
using System.Text;

namespace Cinema
{
    static class UserRegistration
    {
        public static void Start()
        {
            DisplayHeader.RegistrationHeader();
            Console.WriteLine("\n---------------------------------------------------------------------------\n");
            Console.WriteLine("Welcome to the registration page!");
            Console.WriteLine("Please enter your email address and press enter to confirm or press Esc to cancel.");

            Console.Write(">>> ");
            string email = ReadInputOrCancel();

            if (email == null)
            {
                Console.WriteLine("Registration cancelled.");
                return; // Exit the method if registration was cancelled
            }

            Console.Clear();
            DisplayHeader.RegistrationHeader();
            Console.WriteLine("\n---------------------------------------------------------------------------\n");

            // Email input and validation in a loop
            do
            {
                if (!UserRegistrationLogic.ValidateEmail(email))
                {
                    Console.WriteLine("Invalid email. Please ensure the email contains '@gmail.com', '@outlook.com', '@gmail.nl', or '@outownline.nl'.");
                    email = ReadInputOrCancel();
                    if (email == null)
                    {
                        Console.WriteLine("Registration cancelled.");
                        return;
                    }
                }
            } while (!UserRegistrationLogic.ValidateEmail(email));

            // Password input and validation in a loop
            string password = GetValidPassword("Please enter your password:\nMust at least contain 8 characters\nOne capital letter\nOne symbol\nOne number:\nPress Esc key if you want to cancel or Enter to confirm.");
            if (password == null)
            {
                Console.WriteLine("Registration cancelled.");
                return;
            }

            // Confirm password input and validation in a loop
            string confirmPassword;
            do
            {
                confirmPassword = GetPassword("Please re-enter your password for confirmation:");
                if (confirmPassword == null)
                {
                    Console.WriteLine("Registration cancelled.");
                    return;
                }

                if (password != confirmPassword)
                {
                    Console.WriteLine("Passwords do not match. Please try again.");
                }
            } while (password != confirmPassword);

            // Full name input
            string fullName = ReadInputOrCancel("Please enter your full name and press enter to confirm:");
            if (fullName == null)
            {
                Console.WriteLine("Registration cancelled.");
                return;
            }

            // Proceed with the registration
            UserRegistrationLogic.Register(email, password, fullName);
            Console.WriteLine("Registration successful. Welcome, " + fullName);
        }

        private static string ReadInputOrCancel(string prompt = "")
        {
            if (!string.IsNullOrEmpty(prompt))
            {
                Console.WriteLine(prompt);
            }
            StringBuilder input = new StringBuilder();
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter && input.Length > 0)
                {
                    Console.WriteLine();
                    return input.ToString();
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("\nOperation cancelled.");
                    return null;
                }
                else if (key.Key == ConsoleKey.Backspace && input.Length > 0)
                {
                    input.Remove(input.Length - 1, 1);
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    input.Append(key.KeyChar);
                    Console.Write(key.KeyChar);
                }
            }
        }

        private static string GetPassword(string prompt)
        {
            Console.WriteLine($"{prompt}\n");
            Console.Write(">>> ");
            StringBuilder password = new StringBuilder();

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter && password.Length > 0)
                {
                    Console.WriteLine();  // Move to the next line
                    return password.ToString();
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("\nOperation cancelled.");  // Provide feedback
                    return null;  // Return null if Esc is pressed to indicate cancellation
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password.Remove(password.Length - 1, 1);
                    Console.Write("\b \b");  // Remove last character from console display
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    password.Append(key.KeyChar);
                    Console.Write("*");  // Display a placeholder instead of the real character
                }
            }
        }

        private static string GetValidPassword(string prompt)
        {
            string password;
            do
            {
                password = GetPassword(prompt);
                if (password == null) return null; // Return null if Esc is pressed to indicate cancellation

                if (!UserRegistrationLogic.ValidatePassword(password))
                {
                    Console.WriteLine("Password does not meet the requirements. Please try again.");
                }
            } while (!UserRegistrationLogic.ValidatePassword(password));

            return password;
        }
    }
}
