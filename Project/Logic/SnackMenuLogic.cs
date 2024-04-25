namespace Cinema;

public class SnackMenuLogic
{
	const string filePathSnackMenu = "DataStorage/Menu.json";
	public static void ListSnackMenu(bool isvoid)
	{
		if(isvoid){
			List<SnackMenuModel> MenuList = JsonAccess.ReadFromJson<SnackMenuModel>(filePathSnackMenu);
			Console.WriteLine($" __________________________________________________________________________________________________________________________________________________");
			Console.WriteLine($"| {"Snack".PadRight(50)} | {"Price".PadRight(4)}    | {"Description".PadRight(80)} |");
			Console.WriteLine($"|----------------------------------------------------+----------+----------------------------------------------------------------------------------|");

			foreach (SnackMenuModel snack in MenuList)
			{
				Console.WriteLine($"| {snack.Name.PadRight(50)} | $ {Convert.ToString(snack.Price).PadRight(4)}   | {snack.Description.PadRight(80)} |");
			}
			Console.WriteLine($" --------------------------------------------------------------------------------------------------------------------------------------------------");
		}
	}
}