using System;
using System.Text;

namespace Cinema
{
    static class UserRegistration
    {
        public static void Start()
        {
            Console.WriteLine("Welcome to the registration page.");

            // Email input and validation in a loop
            string email;
            do
            {
                email = InputHandler.ReadInputWithCancel("Please enter your email address:");
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
                password = GetValidPassword("Please enter your password:\nMust at least contain 8 characters, one capital letter, one symbol, and one number:");
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
            Console.WriteLine("Please enter your full names:");
            string fullName = InputHandler.ReadInputWithCancel("Please enter your full names:");
            if (fullName == null)
            {
                Console.WriteLine("Registration cancelled.");
                return;
            }

            // Proceed with the registration
            UserRegistrationLogic.Register(email, password, fullName);
            Console.WriteLine("Registration successful. Welcome, " + fullName);
        }

        private static string GetPassword(string prompt)
        {
            return InputHandler.ReadInputWithCancel(prompt);
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