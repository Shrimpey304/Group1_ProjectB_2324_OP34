using Cinema;

public class AdminFuncUI{

	public static void adminChangeSeatTypes(){

		List<string> files = DisplayRoom.getFileDir();

		int cnt = 0;
		foreach(var file in files){
			Console.WriteLine($"roomnumber: {cnt+1}\nfile:{file}");
			Console.WriteLine("----------------------------");
			cnt++;
		}
		Console.Write("room to adjust >> ");
		string inp = Console.ReadLine()!;
		int intinp = Convert.ToInt32(inp);

		string selected = files[intinp-1];

		DisplayRoomUI.changeSeattype(selected);
	}


    public static void adminCreateRoom(){
		DisplayHeaderUI.AdminHeader();
		Console.WriteLine("\n---------------------------------------------------------------------------\n");
		Console.WriteLine("How many rows will your room have\n");
		Console.Write(">>> ");
		string rows = Console.ReadLine()!;
		int introws = Convert.ToInt32(rows);

		Console.Clear();
		DisplayHeaderUI.AdminHeader();
		Console.WriteLine("\n---------------------------------------------------------------------------\n");
		Console.WriteLine("How many Columns will your room have\n");
		Console.Write(">>> ");
		string cols = Console.ReadLine()!;
		int intcols = Convert.ToInt32(cols);

		DisplayRoom.CreateNewDefaultJson(introws, intcols);

		Console.WriteLine("room created succesfully");
	}
}