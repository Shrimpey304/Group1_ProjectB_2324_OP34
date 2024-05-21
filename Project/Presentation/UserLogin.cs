using System;

namespace Cinema;

static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();

    public static void Start()
    {
        Console.WriteLine("Welcome to the login page");
        Console.WriteLine("Please enter your email address:");
        string? email = Console.ReadLine();

        // Use GetPassword method for masked password input
        Console.WriteLine("Please enter your password:");
        string? password = GetPassword();
        
        // Check if password retrieval was cancelled
        if (password == null)
        {
            Console.WriteLine("Login cancelled.");
            return; // Exit the method if login was cancelled
        }

        AccountModel? acc = accountsLogic.CheckLogin(email!, password);
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

    private static string? GetPassword()
    {
        string password = "";
        while (true)
        {
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Enter && password.Length > 0) // Ensure password is not empty
            {
                Console.WriteLine();
                return password;
            }
            else if (key.Key == ConsoleKey.Escape) // Handle the Esc key to cancel the login
            {
                return null; // Return null to indicate cancellation
            }
            else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                password = password[..^1]; // Removes last character
                Console.Write("\b \b"); // Moves the cursor back, writes space to remove the character, then moves back again.
            }
            else if (!char.IsControl(key.KeyChar))
            {
                password += key.KeyChar; // Add character to password
                Console.Write("•"); // Display '•' instead of '*'
            }
        }
    }
}
