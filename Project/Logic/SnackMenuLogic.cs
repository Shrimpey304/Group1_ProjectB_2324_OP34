using System.Net.Sockets;

namespace Cinema;

public class SnackMenuLogic
{
	public static List<Tuple<string, int>>  OrderedSnacks = new();
	public static bool FinishOrder = false;
	const string filePathSnackMenu = "DataStorage/SnackMenu.json";
	public static double TotalCost = 0;
	public static List<SnackMenuModel> menuList = JsonAccess.ReadFromJson<SnackMenuModel>(filePathSnackMenu);	
	
	public static void FinishOrdering()
	{
		FinishOrder = true;
		Console.WriteLine("Your total order:\n");
		if (OrderedSnacks != null){
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
			Console.WriteLine("No snacks to remove.");
			return;
		}
		// Ask user for the key to remove
		Console.Write("\nEnter the number to remove: ");
		string keyToRemove = Console.ReadLine()!;
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
	
	public static void AddSnack(int OrderedSnackInt)
	{
		foreach(SnackMenuModel snack in menuList)
			{
				if (snack.SnackID == OrderedSnackInt)
				{
					Console.Clear();
					Console.WriteLine($"How many {snack.Name} would you like?");
					string SnackAmount = Console.ReadLine()!;
					
					OrderedSnacks ??= new List<Tuple<string, int>>();
					
					int SnackAmountInt = Convert.ToInt32(SnackAmount);
					if (SnackAmountInt <= 0)
					{
						SnackAmountInt = 1;
					}
					var a = new Tuple<string, int>(snack.Name, SnackAmountInt);

					OrderedSnacks.Add(a);

					Console.WriteLine($"Added {SnackAmountInt} {snack.Name} to order.");
					Console.WriteLine();
					Console.WriteLine("Press any key to continue...");
					Console.ReadKey();
					MenuUtils.displaySnackOption();
				}
			}
	}
	
}