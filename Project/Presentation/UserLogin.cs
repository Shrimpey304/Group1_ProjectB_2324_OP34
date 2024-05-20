namespace Cinema;

static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();

    public static void Start()
    {
        DisplayHeader.LoginHeader();
        Console.WriteLine("\n---------------------------------------------------------------------------\n");
        Console.WriteLine("Welcome to the login page!");
        Console.WriteLine("Please enter your email address\n");

        Console.Write(">>> ");
        string? email = Console.ReadLine();

        // Use GetPassword method for masked password input
        Console.Clear();
        DisplayHeader.LoginHeader();
        Console.WriteLine("\n---------------------------------------------------------------------------\n");

        Console.WriteLine("Please enter your password\n");
        string password = GetPassword();

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

    private static string GetPassword()
    {
        Console.Write(">>> ");
        string password = "";
        while (true)
        {
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                return password;
            }
            else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                password = password[..^1]; // Removes last character
                Console.Write("\b \b"); // Moves the cursor back, writes space to remove the asterisk, then moves back again.
            }
            else if (!char.IsControl(key.KeyChar))
            {
                password += key.KeyChar; // Add character to password
                Console.Write("*"); // Mask input
            }
        }
    }
}
