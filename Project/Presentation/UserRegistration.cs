using System;

namespace Cinema
{
    static class UserRegistration
    {
        public static void Start()
        {
            Console.WriteLine("Welcome to the registration page.");
            Console.WriteLine("Please enter your email address:");
            string? email = Console.ReadLine();
            Console.WriteLine("Please enter your password:");
            string? password = Console.ReadLine();
            Console.WriteLine("Please enter your full name:");
            string? fullName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(fullName))
            {
                Console.WriteLine("Invalid input. Please ensure all fields are filled out.");
                return;
            }

            // Call the registration logic
            UserRegistrationLogic.Register(email, password, fullName);

            
        }
    }
}
