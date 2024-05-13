using System;
using System.Collections.Generic;
using System.ComponentModel;
namespace Cinema;

public static class MenuUtils{

	static void PerformAction(Action action)
	{
		Console.Clear();
		action.Invoke();
		Console.WriteLine("\nPress any key to continue...");
		Console.ReadKey();
	}

	static void RunCheckboxMenu(Dictionary<string, Action> optionsAndActions, string headertype)
	{
		int selectedIndex = 0;
		List<string> options = new List<string>(optionsAndActions.Keys);
		bool[] selectedOptions = new bool[options.Count];

		while (true)
		{
			Console.Clear();

			if(headertype == "main"){
				DisplayHeader.HeaderMain();
				if(AccountsLogic.CurrentAccount != null){
				Console.WriteLine($"\nwelcome: {AccountsLogic.CurrentAccount.FullName}");
				}else{
				Console.WriteLine("\nwelcome: user");
				}
			}
			else if(headertype == "snacks"){
				DisplayHeader.HeaderSnack();
				Console.WriteLine("\n\nWould you like to order (more)?");
			}
			
			Console.WriteLine("\n\nuse the arrows and press 'enter' to select option\n");

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

		Dictionary<string, Action> MainMenuOptions = new()
		{ 

			{ "Login", TestLogin },
			{ "Register", TestRegister },
			{ "Display MovieList", () => MovieLogic.ListAllMovies(true)},
			{ "Display Menu",() => SnackMenuLogic.ListSnackMenu()},
			{ "Exit", KillProgram},

		};

		
		RunCheckboxMenu(MainMenuOptions, "main");
		
	}

	public static void displayLoggedinMenu(){

		Dictionary<string, Action> LoginMenuOptions = new()
		{ 

			{ "Reserve Ticket", ReserveTicket.ReserveProcess},
			{ "Show Tickets", AccountsLogic.GetTickets},
			{ "Show Profile", AccountsLogic.getuserinfo},
			{ "Display Menu",() => SnackMenuLogic.ListSnackMenu()},
			{ "Logout", AccountsLogic.logout},
			{ "Exit", KillProgram}

		};

		
		RunCheckboxMenu(LoginMenuOptions, "main");
		
	}

	public static void displayLoggedinAdminMenu(){

		Dictionary<string, Action> LoginMenuOptions = new()
		{ 

			{ "Reserve Ticket", ReserveTicket.ReserveProcess},
			{ "Show Tickets", AccountsLogic.GetTickets},
			{ "Show Profile", AccountsLogic.getuserinfo},
			{ "Display Menu",() => SnackMenuLogic.ListSnackMenu()},
			{ "Random Admin option", AccountsLogic.logout},
			{ "Logout", AccountsLogic.logout},
			{ "Exit", KillProgram}

		};

		
		RunCheckboxMenu(LoginMenuOptions, "main");
		
	}
	
	public static void displaySnackOption(){

		Dictionary<string, Action> DisplaySnack = new()
		{ 

			{ "Add snacks",() => SnackMenuLogic.ListSnackMenu(true)},
			{ "Remove snacks",Console.WriteLine},
			{ "Finish order", () => SnackMenuLogic.FinishOrdering()},

		};
		Console.WriteLine("Would you like to order snacks?\n");
		
		RunCheckboxMenu(DisplaySnack, "snacks");
		
	}

	public static void RandomAdminOption(){
		Console.WriteLine("Random option");
	}
	public static void TestLogin(){
		UserLogin.Start();
	}
	public static void TestRegister(){
		UserRegistration.Start();
	}
	public static void KillProgram(){
		Console.WriteLine("Exiting the program...");
		Environment.Exit(0);
	}
}