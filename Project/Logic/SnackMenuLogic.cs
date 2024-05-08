using System.Security.Cryptography.X509Certificates;

namespace Cinema;
//Seperate Food and drinks!!!


public class SnackMenuLogic
{
	public static Dictionary<int, string>  OrderedSnacks = new Dictionary<int, string>();
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
					if (snack.SnackID == OrderedSnack)
					{
						Console.Clear();
						Console.WriteLine($"How many {snack.Name} would you like?");
						string SnackAmount = Console.ReadLine();
						int SnackAmountInt = Convert.ToInt32(SnackAmount);
						OrderedSnacks.Add(SnackAmountInt, snack.Name);
						Console.WriteLine($"Added {SnackAmountInt} {snack.Name} to order.");
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
		foreach (KeyValuePair <int,string> item in OrderedSnacks)
		{
			foreach(SnackMenuModel snack in menuList)
			{
				if (snack.Name == item.Value)
				{
					int SnackPrice = Convert.ToInt32(snack.Price);
					TotalCost += SnackPrice * item.Key;
				}
			}
		}
		if (OrderedSnacks != null)
		{
			foreach (KeyValuePair <int,string> snack in OrderedSnacks)
			{
				Console.WriteLine($"{snack.Value} {snack.Key}");
			}
		}
		Console.WriteLine($"Total cost:\n${TotalCost}");
		Thread.Sleep(5000);
		MenuUtils.displayLoggedinMenu();
	}
}