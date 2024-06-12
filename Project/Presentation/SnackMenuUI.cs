using System.Linq;
namespace Cinema;
//Seperate Food and drinks!!!


public static class SnackMenuUI
{

	public static void ListSnackMenu(bool isvoid)
	{
		if (isvoid)
		{

			while(SnackMenuLogic.FinishOrder == false)
			{
				string OrderedSnack;
				do{
				Console.Clear();
				Console.WriteLine(" ______________________________________________________________________________________________________________________________________________________");
				Console.WriteLine("| Id | Name                                | Price   | Description                                                                   | Extra Info |");
				Console.WriteLine("|---------+-------------------------------------+---------+-------------------------------------------------------------------------------+------------|");

				foreach (SnackMenuModel snack in SnackMenuLogic.menuList)
				{
					Console.WriteLine($"| {snack.Id.ToString().PadLeft(7)} | {snack.Name.PadRight(35)} | ${snack.Price.ToString("0.00").PadLeft(6)} | {snack.Description.PadRight(77)} | {snack.ExtraInfo.PadRight(10)} |");
				}

				Console.WriteLine(" ------------------------------------------------------------------------------------------------------------------------------------------------------");
				Console.WriteLine("Legend\n[V] = Vegan\n[L] = Contains lactose\n");
				Console.WriteLine("Your current order:\n");
				if (SnackMenuLogic.OrderedSnacks.Count!= 0)
				{
					foreach (Tuple<string,int> snack in SnackMenuLogic.OrderedSnacks)
					{
						Console.WriteLine($"{snack.Item1} [{snack.Item2}x]");
					}
				}
				Console.WriteLine();
				Console.WriteLine("Type the in the Id you want to order");	
				Console.Write(">>>");
				OrderedSnack = Console.ReadLine()!;
				}
				while((OrderedSnack == null) || (MovieLogic.IsDigitsOnly(OrderedSnack)== false) || (OrderedSnack == ""));
				int OrderedSnackInt = Convert.ToInt32(OrderedSnack);
				SnackMenuLogic.AddSnack(OrderedSnackInt);
			}
		}

	}
	
	
	public static void ListSnackMenu()
	{
		Console.WriteLine(" ______________________________________________________________________________________________________________________________________________________");
		Console.WriteLine("| Id      | Name                                | Price   | Description                                                                   | Extra Info |");
		Console.WriteLine("|---------+-------------------------------------+---------+-------------------------------------------------------------------------------+------------|");

		foreach (SnackMenuModel snack in SnackMenuLogic.menuList)
		{
			Console.WriteLine($"| {snack.Id.ToString().PadLeft(7)} | {snack.Name.PadRight(35)} | ${snack.Price.ToString("0.00").PadLeft(6)} | {snack.Description.PadRight(77)} | {snack.ExtraInfo.PadRight(10)} |");
		}

		Console.WriteLine(" ------------------------------------------------------------------------------------------------------------------------------------------------------");
		Console.WriteLine("Legend\n[V] = Vegan\n[L] = Contains lactose\n");
	}
}