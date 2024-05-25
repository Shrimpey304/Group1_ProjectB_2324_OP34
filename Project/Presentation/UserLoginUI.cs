using System;

namespace Cinema;

static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();

    public static void Start()
    {
        DisplayHeaderUI.LoginHeader();
        Console.WriteLine("\n---------------------------------------------------------------------------\n");
        Console.WriteLine("Welcome to the login page!");
        Console.WriteLine("Please enter your email address and press enter to confirm\nPress Esc key to cancel.");
        
        Console.Write(">>> ");
        string? email = ReadEmailOrCancel();

        if (email == null)
        {
            Console.WriteLine("Login cancelled.");
            return; // Exit the method if login was cancelled
        }

        Console.Clear();
        DisplayHeaderUI.LoginHeader();
        Console.WriteLine("\n---------------------------------------------------------------------------\n");

        Console.WriteLine("Please enter your password and press enter to confirm\nPress Esc key to cancel.");
        string password = GetPassword()!;

        if (password == null)
        {
            Console.WriteLine("Login cancelled.");
            return; // Exit the method if login was cancelled
        }

        AccountModel? acc = accountsLogic.CheckLogin(email, password);
        if (acc != null)
        {
            Console.WriteLine($"Welcome back {acc.FullName}");
            if (acc.IsAdmin)
            {
                MenuUtils.displayLoggedinAdminMenu();
            }
            else
            {
                MenuUtils.displayLoggedinMenu();
            }
        }
        else
        {
            Console.WriteLine("No account found with that email and password");
        }
    }

    private static string? ReadEmailOrCancel()
    {
        string email = "";
        while (true)
        {
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Enter && email.Length > 0) // Ensure email is not empty
            {
                Console.WriteLine();
                return email;
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                return null; // Return null to indicate cancellation
            }
            else if (key.Key == ConsoleKey.Backspace && email.Length > 0)
            {
                email = email[..^1]; // Removes last character
                Console.Write("\b \b"); // Removes the character visually
            }
            else if (!char.IsControl(key.KeyChar))
            {
                email += key.KeyChar; // Add character to email
                Console.Write(key.KeyChar); // Display the character
            }
        }
    }

    private static string? GetPassword()
    {
        Console.Write(">>> ");
        string password = "";
        while (true)
        {
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Enter && password.Length > 0)
            {
                Console.WriteLine();
                return password;
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                return null; // Return null to indicate cancellation
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
    }
}
