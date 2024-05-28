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
				DisplayHeaderUI.HeaderMain();
				if(AccountsLogic.CurrentAccount != null){
				Console.WriteLine($"\nwelcome: {AccountsLogic.CurrentAccount.FullName}");
				}else{
				Console.WriteLine("\nwelcome: user");
				}
			}
			else if(headertype == "snacks"){
				DisplayHeaderUI.HeaderSnack();
				Console.WriteLine("\n\nWould you like to order (more)?");
			}
			else if(headertype == "admin"){
				DisplayHeaderUI.AdminHeader();
				Console.WriteLine($"\nwelcome admin: {AccountsLogic.CurrentAccount!.FullName}");
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

			{ "Login", UserLogin.Start },
			{ "Register", UserRegistration.Start },
			{ "Display MovieList", () => MovieUI.ListAllMovies(true)},
			{ "Display Menu", SnackMenuLogic.ListSnackMenu},
			{ "Exit", KillProgram},

		};

		
		RunCheckboxMenu(MainMenuOptions, "main");
		
	}

	public static void displayLoggedinMenu(){

		Dictionary<string, Action> LoginMenuOptions = new()
		{ 

			{ "Reserve Ticket", TicketLogic.ReserveTicket},
			{ "Show Tickets", AccountsLogic.GetTickets},
			{ "Show Profile", AccountsLogic.getuserinfo},
			{ "Show MovieList", () => MovieUI.ListAllMovies(true)},
			{ "Show Snack Menu", SnackMenuLogic.ListSnackMenu},
			{ "Logout", AccountsLogic.logout},
			{ "Exit", KillProgram}

		};

		
		RunCheckboxMenu(LoginMenuOptions, "main");
		
	}

	public static void displayLoggedinAdminMenu(){

		Dictionary<string, Action> LoginMenuOptions = new()
		{ 

			{ "Change Seat Types", AdminFuncUI.adminChangeSeatTypes},
			{ "Create New Room", AdminFuncUI.adminCreateRoom},
			{ "Add Movie", AdminFuncUI.adminAddMovie},
			{ "Add Session", AdminFuncUI.adminAddSession},
			{ "Show Tickets", AccountsLogic.GetTickets},
			{ "Show Profile", AccountsLogic.getuserinfo},
			{ "Display Menu", SnackMenuLogic.ListSnackMenu},
			{ "Logout", AccountsLogic.logout},
			{ "Exit", KillProgram}

		};

		
		RunCheckboxMenu(LoginMenuOptions, "admin");
		
	}
	
	public static void displaySnackOption(){

		Dictionary<string, Action> DisplaySnack = new()
		{ 

			{ "Add snacks",() => SnackMenuLogic.ListSnackMenu(true)},
			{"Remove snack", SnackMenuLogic.RemoveSnack},
			{ "Finish order", SnackMenuLogic.FinishOrdering},

		};
		Console.WriteLine("Would you like to order snacks?\n");
		if (SnackMenuLogic.FinishOrder != true)
		{
			RunCheckboxMenu(DisplaySnack, "snacks");
		}
		return;
		
		
	}

	public static void KillProgram(){
		Console.WriteLine("Exiting the program...");
		Environment.Exit(0);
	}
}