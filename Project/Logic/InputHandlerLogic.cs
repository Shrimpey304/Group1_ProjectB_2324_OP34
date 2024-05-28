using System;
using System.Text;

namespace Cinema;

public static class InputHandler
{
    // Method to read input with an option to cancel using the Esc key
    public static string ReadInputWithCancel(string prompt)
    {
        Console.WriteLine(prompt);
        StringBuilder input = new StringBuilder();
        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Enter && input.Length > 0)
            {
                Console.WriteLine(); // Move to the next line
                return input.ToString();
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                Console.WriteLine("\nOperation cancelled."); // Provide feedback
                MenuUtils.displayMainMenu(); // Return null if Esc is pressed to indicate cancellation
            }
            else if (key.Key == ConsoleKey.Backspace && input.Length > 0)
            {
                input.Remove(input.Length - 1, 1);
                Console.Write("\b \b"); // Remove last character from console
            }
            else if (!char.IsControl(key.KeyChar))
            {
                input.Append(key.KeyChar);
                Console.Write(key.KeyChar);
            }
        }
    }

    public static string ReadInputWithCancelLoggedIn(string prompt)
    {
        Console.WriteLine(prompt);
        StringBuilder input = new StringBuilder();
        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Enter && input.Length > 0)
            {
                Console.WriteLine(); // Move to the next line
                return input.ToString();
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                Console.WriteLine("\nOperation cancelled."); // Provide feedback
                MenuUtils.displayLoggedinMenu(); // Return null if Esc is pressed to indicate cancellation
            }
            else if (key.Key == ConsoleKey.Backspace && input.Length > 0)
            {
                input.Remove(input.Length - 1, 1);
                Console.Write("\b \b"); // Remove last character from console
            }
            else if (!char.IsControl(key.KeyChar))
            {
                input.Append(key.KeyChar);
                Console.Write(key.KeyChar);
            }
        }
    }
}


