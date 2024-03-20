namespace Cinema;

public class DisplayRoom{

	public static void DisplaySeatingIDs(List<Dictionary<string, string>>[,] seatingArrangement, int rows, int columns){

		Console.WriteLine("Seating IDs Grid:");

		for (int r = 0; r < rows; r++){
			
			for (int c = 0; c < columns; c++){

				List<Dictionary<string, string>> seatingList = seatingArrangement[r, c];

				foreach (var seatingInfo in seatingList)
				{
					string id = seatingInfo["ID"];
					Console.Write($"[{id.PadLeft(3)}]");
				}
			}
			Console.WriteLine();
		}
	}
}