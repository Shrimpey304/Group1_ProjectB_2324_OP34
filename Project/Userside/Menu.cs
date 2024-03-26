using System;
using Cinema; 
public static class Menu
{
    public static void Start()
    {
        Console.WriteLine("Enter 1 to login");
        Console.WriteLine("Enter 2 to register");
        Console.WriteLine("Enter 3 for other options");

        var input = Console.ReadLine();
        switch (input)
        {
            case "1":
                UserLogin.Start();
                break;
            case "2":
                Register();
                break;
            case "3":
                // Implement other options
                Console.WriteLine("Other options are not yet implemented.");
                break;
            default:
                Console.WriteLine("Invalid input.");
                Start();
                break;
        }
    }

    private static void Register()
    {
        Console.WriteLine("Enter your email address:");
        var email = Console.ReadLine();
        Console.WriteLine("Enter your password:");
        var password = Console.ReadLine();
        Console.WriteLine("Enter your full name:");
        var fullName = Console.ReadLine();

        UserRegistration.Register(email, password, fullName);
        Console.WriteLine("Registration successful.");

        Start(); // Return to the main menu
    }
}
