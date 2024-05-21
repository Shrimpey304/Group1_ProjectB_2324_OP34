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
            Console.WriteLine("Please enter your email address\n");

            Console.Write(">>> ");
            string email = Console.ReadLine()!;

            Console.Clear();
            DisplayHeader.RegistrationHeader();
            Console.WriteLine("\n---------------------------------------------------------------------------\n");

            // Email input and validation in a loop
            string email;
            do
            {
                email = InputHandler.ReadInputWithCancel("Please enter your email address:\nPress esc key if you want to cancel.");
                if (email == null)
                {
                    Console.WriteLine("Registration cancelled.");
                    return;
                }

                if (!UserRegistrationLogic.ValidateEmail(email))
                {
                    Console.WriteLine("Invalid email. Please ensure the email contains '@gmail.com', '@outlook.com', '@gmail.nl', or '@outownline.nl'.");
                }
            } while (!UserRegistrationLogic.ValidateEmail(email));

            // Password input and validation in a loop
            string password;
            do
            {
                password = GetValidPassword("Please enter your password:\nMust at least contain 8 characters\nOne capital letter\nOne symbol\nOne number:\nPress esc key if you want to cancel.");
                if (password == null) // Check if the user pressed Esc to quit during password input
                {
                    Console.WriteLine("Registration cancelled.");
                    return;
                }
            } while (!UserRegistrationLogic.ValidatePassword(password));

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
            string fullName = InputHandler.ReadInputWithCancel("Please enter your full names:");
            if (fullName == null)
            {
                return;
            }

            Console.WriteLine("Please enter your full name:");
            string fullName = Console.ReadLine()!;


            // Proceed with the registration
            UserRegistrationLogic.Register(email, password, fullName);
            Console.WriteLine("Registration successful. Welcome, " + fullName);
        }

        private static string GetPassword(string prompt)
        {

            Console.WriteLine($"{prompt}\n");
            Console.Write(">>> ");
            string password = "";

            Console.WriteLine(prompt);


            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true); // True to not display the pressed key in the console
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
                    Console.Write("•");  // Display a placeholder instead of the real character
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
