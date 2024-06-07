using System.Data.Common;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
namespace Cinema;

public static class DisplayRoom{

	public static double NORMAL_SEAT_PRICE = 9.0;
	public static double DELUXE_SEAT_PRICE = 12.0;
	public static double PREMIUM_SEAT_PRICE = 15.0;


	/// <summary>
	/// Gets the file directory for a specific movie session.
	/// </summary>
	/// <param name="session">The movie session model.</param>
	/// <returns>The file directory as a string.</returns>
	public static string getFileDir(MovieSessionModel session){

		string fileNM = "";
		string DataStoragePath = @"DataStorage/"; 
		string[] filelist = Directory.GetFiles(DataStoragePath);


		if (filelist == null){
			return null!;
		}

		foreach(string file in filelist){
			try{

				if (file.ToLower() != $"datastorage/cinemaroom{Convert.ToString(session.RoomID)}.json"){
					continue;
				}

				List<Seating>fileJson = JsonAccess.ReadFromJson<Seating>($"{file}");

				if (fileJson != null && fileJson.Count > 0){
					fileNM = file;
				}else{
					continue;
				}
			}catch(Exception e){
				Console.WriteLine(e);
				continue;
			}
		}
		return fileNM;
	}

	/// <summary>
	/// Gets the file directories for all cinema rooms.
	/// </summary>
	/// <returns>A list of file directories as strings.</returns>
	public static List<string> getFileDir(){

		string DataStoragePath = @"DataStorage/"; 
		string[] filelist = Directory.GetFiles(DataStoragePath);
		List<string> listOfRooms = new();

		if (filelist == null){
			return null!;
		}

		foreach(string file in filelist){
			try{

				if (file.ToLower().Contains($"datastorage/cinemaroom")){
					List<Seating>fileJson = JsonAccess.ReadFromJson<Seating>($"{file}");

					if (fileJson != null && fileJson.Count > 0){
						listOfRooms.Add(file);
					}else{
						continue;
					}
				}

			}catch(Exception e){
				Console.WriteLine(e);
				continue;
			}
		}
		return listOfRooms;
	}


	/// <summary>
	/// Handles seat selection when the Enter key is pressed.
	/// </summary>
	/// <param name="seating">The seating object.</param>
	/// <param name="SelectedPositions">The list of selected seating positions.</param>
	/// <param name="selectedPositionRow">The selected row position.</param>
	/// <param name="selectedPositionCol">The selected column position.</param>
	/// <param name="session">The movie session model.</param>
	public static void KeyEnterController(Seating seating, List<Tuple<int, int>> SelectedPositions, int selectedPositionRow, int selectedPositionCol, MovieSessionModel session)
	{
		SeatInfo cursorOnSeatingPosition = seating!.SeatingArrangement[selectedPositionRow, selectedPositionCol][0];

		if (cursorOnSeatingPosition.Type == SeatType.NoSeat)
		{
			return;
		}

		if (cursorOnSeatingPosition.reservedInSessionID.Contains(session.sessionID))
		{
			Console.WriteLine("Can't reserve a seat that is already reserved.");
			return;
		}

		if (!cursorOnSeatingPosition.inPrereservation)
		{
			if (SelectedPositions.Count == 0 || SelectedSeatsInRow(SelectedPositions, selectedPositionRow, selectedPositionCol, seating))
			{
				cursorOnSeatingPosition.inPrereservation = true;
				SelectedPositions.Add(new Tuple<int, int>(selectedPositionRow, selectedPositionCol));
			}
			else
			{
				cursorOnSeatingPosition.inPrereservation = false;
				Console.WriteLine("You can only select multiple connecting seats in the same row.");
			}
		}
		else
		{
			var sortedPositions = SelectedPositions.OrderBy(pos => pos.Item2).ToList();

			// Check if the current seat is on the edge
			bool isOnEdge = sortedPositions.First().Item2 == selectedPositionCol || sortedPositions.Last().Item2 == selectedPositionCol;

			if (isOnEdge || SelectedPositions.Count == 1)
			{
				cursorOnSeatingPosition.inPrereservation = false;
				SelectedPositions.Remove(new Tuple<int, int>(selectedPositionRow, selectedPositionCol));
			}
			else
			{
				// Check if the seat is between two selected seats
				int index = sortedPositions.FindIndex(pos => pos.Item2 == selectedPositionCol);
				if (index > 0 && index < sortedPositions.Count - 1)
				{
					cursorOnSeatingPosition.inPrereservation = true;
					Console.WriteLine("This seat is between two selected seats and cannot be deselected.");
				}
				else
				{
					cursorOnSeatingPosition.inPrereservation = false;
					SelectedPositions.Remove(new Tuple<int, int>(selectedPositionRow, selectedPositionCol));
				}
			}
		}
	}

	/// <summary>
	/// Reserves selected seats when the R key is pressed.
	/// </summary>
	/// <param name="SelectedPositions">The list of selected seating positions.</param>
	/// <param name="seating">The seating object.</param>
	/// <param name="session">The movie session model.</param>
	/// <param name="fileNM">The file directory.</param>
	public static void KeyRController(List<Tuple<int,int>> SelectedPositions, Seating seating, MovieSessionModel session, string fileNM){

		foreach(Tuple<int,int> pos in SelectedPositions){

			var selectedSeatingPosition = seating!.SeatingArrangement[pos.Item1, pos.Item2][0];

			selectedSeatingPosition.reservedInSessionID.Add(session.sessionID);
			selectedSeatingPosition.inPrereservation = false;
			List<Seating> uploadSeatingReserved = new(){seating!};

			JsonAccess.UploadToJson(uploadSeatingReserved, fileNM);
		}

		Console.WriteLine("\nGenerating ticket for selected seats");
		Thread.Sleep(2000);

	}

	/// <summary>
	/// Cancels seat reservation when the Backspace key is pressed.
	/// </summary>
	/// <param name="seating">The seating object.</param>
	/// <param name="fileNM">The file directory.</param>
	public static void KeyBackspaceController(Seating seating, string fileNM){
		Console.WriteLine("Cancelling process...");

		for (int i = 0; i < seating.Rows; i++)
		{
			for (int j = 0; j < seating.Columns; j++){

				var seat = seating.SeatingArrangement[i,j][0];

				if(seat.inPrereservation){
					seat.inPrereservation = false;
				}
			}
		}

		List<Seating> undoUploadSeating = new(){seating!};

		JsonAccess.UploadToJson(undoUploadSeating, fileNM);

		MenuUtils.displayLoggedinMenu();
	}

	public static void getColCount(Seating seating){

		Console.Clear();

		Console.ResetColor();
		Console.WriteLine($"\n{"Screen".PadLeft((int)((seating.Columns * 2.5) + 3))}");
		Console.Write("".PadLeft(3));

		for (int col = 0; col < seating.Columns; col++){
			Console.ResetColor();
			Console.Write("_____");
		}
		
		Console.WriteLine("\n");

		Console.Write("".PadLeft(3));
		for (int col = 0; col < seating.Columns; col++){
			Console.ResetColor();

			if(col >= 10){

				Console.Write($" {col+1}  ");

			}else if(col >= 100){

				Console.Write($" {col+1} ");

			}else{

				Console.Write($"  {col+1}  ");

			}
		}
		Console.WriteLine("");
	}

	public static double getSeatPricing(List<Tuple<int,int>> selectedSeats, MovieSessionModel session){

		double totalSeatPrice = 0.0;

		string fileNM = getFileDir(session);

		List<Seating> seatingJson = JsonAccess.ReadFromJson<Seating>(fileNM);
		Seating seating = seatingJson[0];
		try{	

			foreach (var seat in selectedSeats)
			{
				totalSeatPrice += seating.SeatingArrangement[seat.Item1, seat.Item2][0].Price;
			}
			
		}catch(NullReferenceException e){
			Console.WriteLine(e);
		}

		return totalSeatPrice;
	}


	public static void SetColor(int SelectedPositionCol, int selectedPositionRow, Seating seating, MovieSessionModel session){


		for (int i = 0; i < seating.Rows; i++)
		{
			Console.ResetColor();
			Console.Write($"{i+1}".PadRight(3)); //displays rownumber
			for (int j = 0; j < seating.Columns; j++)
			{
				bool isSelected = seating.SeatingArrangement[i,j] == seating.SeatingArrangement[SelectedPositionCol, selectedPositionRow];
				processSeat(i, j, seating, session, isSelected);
			}
			Console.WriteLine("");
		}
	}


	public static void SetColor(int SelectedPositionCol, int selectedPositionRow, Seating seating){


		for (int i = 0; i < seating.Rows; i++)
		{
			Console.ResetColor();
			Console.Write($"{i+1}".PadRight(3)); //displays rownumber
			for (int j = 0; j < seating.Columns; j++)
	        {
            	bool isSelected = seating.SeatingArrangement[i,j] == seating.SeatingArrangement[SelectedPositionCol, selectedPositionRow];
            	seatColor(seating.SeatingArrangement[i,j][0], isSelected);
        	}
			Console.WriteLine("");
		}
	}
	

	public static void processSeat(int i, int j, Seating seating, MovieSessionModel session, bool color){

		if (seating.SeatingArrangement[i, j][0].reservedInSessionID.Any(thisSessionID => thisSessionID == session.sessionID))
		{
			Console.BackgroundColor = ConsoleColor.DarkGray;
			Console.Write($"[ R ]");
		}
		else
		{
			seatColor(seating.SeatingArrangement[i, j][0], color);
		}
	}


	public static void seatColor(SeatInfo seatinfo, bool OverrideColor)
	{
		ConsoleColor color;
		string seatType;

		if (seatinfo.inPrereservation)
		{
			color = OverrideColor ? ConsoleColor.Green : ConsoleColor.DarkMagenta;
			seatType = "[ S ]";
		}
		else
		{
			switch (seatinfo.Type)
			{
				case SeatType.Normal:
					color = OverrideColor ? ConsoleColor.Green : ConsoleColor.Yellow;
					seatType = "[ N ]";
					break;
				case SeatType.Deluxe:
					color = OverrideColor ? ConsoleColor.Green : ConsoleColor.Blue;
					seatType = "[ D ]";
					break;
				case SeatType.Premium:
					color = OverrideColor ? ConsoleColor.Green : ConsoleColor.Red;
					seatType = "[ P ]";
					break;
				case SeatType.NoSeat:
					color = OverrideColor ? ConsoleColor.Green : ConsoleColor.Black;
					seatType = "     ";
					break;
				default:
					throw new ArgumentException($"Invalid seat type: {seatinfo.Type}");
			}
		}

		Console.BackgroundColor = color;
		Console.Write(seatType);
	}


	public static bool SelectedSeatsInRow(List<Tuple<int, int>> selectedPositions, int selectedPositionRow, int selectedPositionCol, Seating seating)
	{
		if (selectedPositions.Count >= 8 || selectedPositions.Count == 0)
		{
			return selectedPositions.Count == 0;
		}

		var sortedPositions = selectedPositions.OrderBy(pos => pos.Item2).ToList();
		var firstCol = sortedPositions.First().Item2;
		var lastCol = sortedPositions.Last().Item2;

		if (firstCol == 0 || lastCol == seating.Columns - 1)
		{
			return true;
		}

		for (int i = 0; i < sortedPositions.Count - 1; i++)
		{
			if (sortedPositions[i + 1].Item2 - sortedPositions[i].Item2 != 1)
			{
				return false;
			}
		}

		return sortedPositions.Any(pos => pos.Item1 == selectedPositionRow && Math.Abs(pos.Item2 - selectedPositionCol) == 1);
	}

	public static void changeSeatprice(SeatInfo seat){

		switch(seat.Type){
			case SeatType.Normal:
				seat.Price = NORMAL_SEAT_PRICE;
				break;
			case SeatType.Deluxe:
				seat.Price = DELUXE_SEAT_PRICE;
				break;
			case SeatType.Premium:
				seat.Price = PREMIUM_SEAT_PRICE;
				break;
			case SeatType.NoSeat:
				seat.Price = 0.0;
				break;
		}
	}


	/// <summary>
	/// used for creating new cinema rooms
	/// </summary>
	/// <param name="rows"></param>
	/// <remarks> to create a new room json file: string "DataStorage/yourjsonname.json" </remarks>
	/// <param name="cols"></param>
	/// <param name="filePath"></param>
	public static void CreateNewDefaultJson(int rows, int cols)
	{
		string DataStoragePath = @"DataStorage/";

		List<Seating> seatingInstance = new();

		string[] filelist = Directory.GetFiles(DataStoragePath);
		List<int> cinemaroomlist = new();

		foreach(string file in filelist)
		{
			if (file.ToLower().Contains("cinemaroom"))
			{
				string[] splitfile = file.ToLower().Split("cinemaroom");
				string[] splitfileID = splitfile[1].Split(".json");
				int fileID = Convert.ToInt32(splitfileID[0]);
				cinemaroomlist.Add(fileID);
			}
		}

		int id = cinemaroomlist.Any() ? cinemaroomlist.Max() + 1 : 1;
		Seating seating = new Seating(rows, cols, id);

		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < cols; j++)
			{
				if (seating.SeatingArrangement[i, j] == null)
				{
					seating.SeatingArrangement[i, j] = new List<SeatInfo>();
				}
				seating.SeatingArrangement[i, j].Add(new SeatInfo
				{
					RowID = i,
					ColumnID = j,
					Price = NORMAL_SEAT_PRICE,
					reservedInSessionID = new(),
					Type = SeatType.Normal,
					inPrereservation = false
				});
			}
		}

		string newPath = $"{DataStoragePath}/CinemaRoom{id}.json";
		seatingInstance.Add(seating);
		JsonAccess.UploadToJson(seatingInstance, newPath);
	}
	
	public static int GetValidSize(string prompt)
	{
		int value;
		while (true)
		{
			Console.WriteLine(prompt);
			Console.Write(">>> ");
			string input = Console.ReadLine()!;

			if (int.TryParse(input, out value) && value >= 0 && value <= 35)
			{
				break;
			}
			else
			{
				Console.WriteLine("Invalid input. Please enter a number between 0 and 35.");
			}
		}
		return value;
	}

}