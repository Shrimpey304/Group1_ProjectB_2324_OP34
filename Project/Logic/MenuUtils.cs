using System;
using System.Collections.Generic;
namespace Cinema;

public static class MenuUtils{

    static void PerformAction(Action action)
    {
        Console.Clear();
        action.Invoke();
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    static void RunCheckboxMenu(Dictionary<string, Action> optionsAndActions)
    {
        int selectedIndex = 0;
        List<string> options = new List<string>(optionsAndActions.Keys);
        bool[] selectedOptions = new bool[options.Count];

        while (true)
        {
            Console.Clear();

            Console.WriteLine("press 'enter' to select option");

            for (int i = 0; i < options.Count; i++)
            {
                Console.Write(selectedIndex == i ? "[x] " : "[ ] ");
                Console.WriteLine(options[i]);
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    selectedIndex = (selectedIndex - 1 + options.Count) % options.Count;
                    break;

                case ConsoleKey.DownArrow:
                    selectedIndex = (selectedIndex + 1) % options.Count;
                    break;

                case ConsoleKey.Enter:
                    PerformAction(optionsAndActions[options[selectedIndex]]);
                    break;
            }
        }
    }

    public static void displayMainMenu(){

        Dictionary<string, Action> MainMenuOptions = new(){ { "a", DisplaySeatingTest }, {"b", TestLogin} };

        RunCheckboxMenu(MainMenuOptions);
        
    }


    public static void DisplaySeatingTest(){
        const string fileName = "DataStorage/CinemaRoom1.json";
        const string fileNameSesh = "DataStorage/Sessions.json";
        //waiting for logic to select room by movie/session
        
        List<MovieSession> sessions = SessionsJsonUtils.ReadFromJson(fileNameSesh);
        DisplayRoom.SelectSeating(fileName, sessions[0]);
    }
    
    public static void TestLogin(){
        Console.WriteLine("test");
    }
}