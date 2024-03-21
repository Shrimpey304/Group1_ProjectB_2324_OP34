using System;
using Cinema; 

namespace Cinema
{
    public class LoginMenu
    {
        public static void RunApplication()
        {
            UserLoginCheck userLoginCheck = new UserLoginCheck();

            Console.WriteLine("Choose an option:");
            Console.WriteLine("1: Register");
            Console.WriteLine("2: Login");
            string? option = Console.ReadLine();

            Console.WriteLine("Enter username >");
            string? username = Console.ReadLine();
            Console.WriteLine("Enter password >");
            string? password = Console.ReadLine();

            if (option == "1")
            {
                userLoginCheck.RegisterUser(username, password);
                Console.WriteLine("Registration successful.");
            }
            else if (option == "2")
            {
                if (userLoginCheck.ValidateLogin(username, password))
                {
                    Console.WriteLine("Login successful.");
                }
                else
                {
                    Console.WriteLine("Login failed.");
                }
            }
        }
    }
}
