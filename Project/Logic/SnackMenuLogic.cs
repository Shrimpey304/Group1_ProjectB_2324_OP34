using System.Linq;
namespace Cinema;
//Seperate Food and drinks!!!


public static class SnackMenuLogic
{
	public static List<Tuple<string, int>>  OrderedSnacks = new();
	public static bool FinishOrder = false;
	const string filePathSnackMenu = "DataStorage/Menu.json";
	public static double TotalCost = 0;
	public static List<SnackMenuModel> menuList = JsonAccess.ReadFromJson<SnackMenuModel>(filePathSnackMenu);	
	
	public static void ListSnackMenu(bool isvoid)
	{
		if (isvoid)
		{

			while(FinishOrder == false)
			{
				Console.WriteLine(" ______________________________________________________________________________________________________________________________________________________");
				Console.WriteLine("| SnackID | Name                                | Price   | Description                                                                   | Extra Info |");
				Console.WriteLine("|---------+-------------------------------------+---------+-------------------------------------------------------------------------------+------------|");

				foreach (SnackMenuModel snack in menuList)
				{
					Console.WriteLine($"| {snack.SnackID.ToString().PadLeft(7)} | {snack.Name.PadRight(35)} | ${snack.Price.ToString("0.00").PadLeft(6)} | {snack.Description.PadRight(77)} | {snack.ExtraInfo.PadRight(10)} |");
				}

				Console.WriteLine(" ------------------------------------------------------------------------------------------------------------------------------------------------------");
				Console.WriteLine("Legend\n[V] = Vegan\n[L] = Contains lactose\n");
				string OrderedSnack;
				do{
				Console.WriteLine("Type the in the SnackID you want to order");	
				OrderedSnack = Console.ReadLine();
				}
				while((OrderedSnack == null) || (MovieLogic.IsDigitsOnly(OrderedSnack)== false) || (OrderedSnack == ""));
				int OrderedSnackInt = Convert.ToInt32(OrderedSnack);
				
				foreach(SnackMenuModel snack in menuList)
				{
					if (snack.SnackID == OrderedSnackInt)
					{
						Console.Clear();
						Console.WriteLine($"How many {snack.Name} would you like?");
						string SnackAmount = Console.ReadLine();
						// Initialize OrderedSnacks if it is null
						OrderedSnacks ??= new List<Tuple<string, int>>();
						// try
						// {
						int SnackAmountInt = Convert.ToInt32(SnackAmount);
						var a = new Tuple<string, int>(snack.Name, SnackAmountInt);

						OrderedSnacks.Add(a);

						Console.WriteLine($"Added {SnackAmountInt} {snack.Name} to order.");
						// }
						// catch (Exception e)
						// {
						// 	Console.WriteLine("Error message: " + e.Message);
						// }
						
						return;
					}
				}
			}
		}

	}
	
	
	public static void ListSnackMenu()
	{
		Console.WriteLine(" ______________________________________________________________________________________________________________________________________________________");
		Console.WriteLine("| SnackID | Name                                | Price   | Description                                                                   | Extra Info |");
		Console.WriteLine("|---------+-------------------------------------+---------+-------------------------------------------------------------------------------+------------|");

		foreach (SnackMenuModel snack in menuList)
		{
			Console.WriteLine($"| {snack.SnackID.ToString().PadLeft(7)} | {snack.Name.PadRight(35)} | ${snack.Price.ToString("0.00").PadLeft(6)} | {snack.Description.PadRight(77)} | {snack.ExtraInfo.PadRight(10)} |");
		}

		Console.WriteLine(" ------------------------------------------------------------------------------------------------------------------------------------------------------");
		Console.WriteLine("Legend\n[V] = Vegan\n[L] = Contains lactose\n");
	}
	
	public static void FinishOrdering()
	{
		FinishOrder = true;
		Console.WriteLine("Your total order:\n");
		foreach (Tuple<string, int> item in OrderedSnacks)
		{
			foreach(SnackMenuModel snack in menuList)
			{
				if (snack.Name == item.Item1)
				{
					int SnackPrice = Convert.ToInt32(snack.Price);
					TotalCost += SnackPrice * item.Item2;
				}
			}
		}
		if (OrderedSnacks != null)
		{
			foreach (Tuple<string,int> snack in OrderedSnacks)
			{
				Console.WriteLine($"{snack.Item1} [{snack.Item2}x]");
			}
		}
		Console.WriteLine($"Total cost:\n${TotalCost}");
		Console.WriteLine("Press any key to confirm your order");
		Console.ReadKey();
		TicketLogic.AddReservation();
	}
	
	public static void RemoveSnack()
	{
		if (OrderedSnacks.Count != 0){
			int counter = 0;
			Console.WriteLine("Current Dictionary:");
			foreach (Tuple<string,int> snack in OrderedSnacks)
			{
				Console.WriteLine($"{counter++} {snack.Item1} [{snack.Item2}x]");
			}
		}
		else
		{
			Console.WriteLine("No snacks ordered.");
			return;
		}
		// Ask user for the key to remove
		Console.Write("\nEnter the number to remove: ");
		string keyToRemove = Console.ReadLine();
		int keyToRemoveInt = Convert.ToInt32(keyToRemove);

		if (keyToRemoveInt >= 0 && keyToRemoveInt < OrderedSnacks.Count)
		{
			// Select the key-value pair at the specified index
			Tuple<string, int> pairToRemove = OrderedSnacks.ElementAt(keyToRemoveInt);

			// Remove the key-value pair
			OrderedSnacks.Remove(pairToRemove);
			Console.WriteLine($"Key '{pairToRemove.Item1}' removed successfully.");
		}
		else
		{
			Console.WriteLine($"Invalid index. Index should be between 0 and {OrderedSnacks.Count - 1}.");
		}
	} 
}