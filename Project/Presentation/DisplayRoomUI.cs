using System.Data.Common;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
namespace Cinema;

public static class DisplayRoomUI{

	/// <summary>
	/// Allows users to select seating for a movie session.
	/// </summary>
	/// <param name="session">The movie session model.</param>
	/// <returns>A list of selected seating positions as tuples.</returns>
	public static List<Tuple<int,int>> SelectSeating(MovieSessionModel session){

		string fileNM = DisplayRoom.getFileDir(session);

		int selectedPositionCol = 0;
		int selectedPositionRow = 0;

		//item 1 = row    item 2 = col
		List<Tuple<int,int>> SelectedPositions = new();   

		List<Seating> SeatingJson = JsonAccess.ReadFromJson<Seating>(fileNM); //now the only seating instead of the temporary one every loop
		Seating seating = SeatingJson[0]; 

		if (seating == null)
		{
			Console.WriteLine("Failed to load seating arrangement from JSON.");
			Thread.Sleep(2000);
			return null!;
		}

		
		while(!Console.KeyAvailable){
			
			DisplayRoom.getColCount(seating);
			
			DisplayRoom.SetColor(selectedPositionRow, selectedPositionCol, seating, session);

			Legenda(SelectedPositions!);

			switch(Console.ReadKey(true).Key){	//controller
				case ConsoleKey.UpArrow:
					selectedPositionRow = Math.Max(0, selectedPositionRow - 1);
				break;
				case ConsoleKey.DownArrow:
					selectedPositionRow = Math.Min(seating.Rows - 1, selectedPositionRow + 1);
				break;
				case ConsoleKey.LeftArrow:
					selectedPositionCol = Math.Max(0, selectedPositionCol - 1);
				break;
				case ConsoleKey.RightArrow:
					selectedPositionCol = Math.Min(seating.Columns - 1, selectedPositionCol + 1);
				break;
				case ConsoleKey.Enter: //select seat

					DisplayRoom.KeyEnterController(seating, SelectedPositions!, selectedPositionRow, selectedPositionCol, session);

				break;
				case ConsoleKey.Backspace: //cancel reservation

					DisplayRoom.KeyBackspaceController(seating, fileNM);

				break;
				case ConsoleKey.R: //reserve selected seats
					
					DisplayRoom.KeyRController(SelectedPositions, seating, session, fileNM);

				return SelectedPositions;
			}

			List<Seating> TempUploadSeating = new(){seating!};

			JsonAccess.UploadToJson(TempUploadSeating, fileNM);
		}
		return null!;
	}

	public static void Legenda(List<Tuple<int,int>> SelectedPositions){
		Console.BackgroundColor = ConsoleColor.Yellow; Console.Write("\n\n[N]".PadLeft(3)); Console.ResetColor(); Console.Write($" = Normal seat  ({DisplayRoom.NORMAL_SEAT_PRICE} euro)\n");
		Console.BackgroundColor = ConsoleColor.Blue; Console.Write("[D]"); Console.ResetColor(); Console.Write($" = Deluxe seat  ({DisplayRoom.DELUXE_SEAT_PRICE} euro)\n");
		Console.BackgroundColor = ConsoleColor.Red; Console.Write("[P]"); Console.ResetColor(); Console.Write($" = Premium seat  ({DisplayRoom.PREMIUM_SEAT_PRICE} euro)\n");
		Console.BackgroundColor = ConsoleColor.DarkGray; Console.Write("[R]"); Console.ResetColor(); Console.Write(" = Reserved seat  \n");
		Console.BackgroundColor = ConsoleColor.Magenta; Console.Write("[S]"); Console.ResetColor(); Console.Write(" = Selected seat  \n");
		Console.BackgroundColor = ConsoleColor.Black; Console.Write("---"); Console.ResetColor(); Console.Write(" = Unselectable place  \n");
		Console.Write("_____"); Console.ResetColor(); Console.Write(" = Screen  \n");
		Console.Write("Press Backspace to cancel and go back to the main menu\n");
		Console.Write("Use the arrow keys to navigate the seats\n");
		Console.Write("Press Enter to select a seat (max 8 in a row)\n");
		Console.Write("Press R to reserve selected seats\n");
		if(SelectedPositions!.Count > 0){

			Console.Write($"Selected seats: Row: [{SelectedPositions[0].Item1 +1}] Seat ");

			foreach(Tuple<int,int> seatLoc in SelectedPositions){

				Console.Write($" [{seatLoc.Item2 +1}]");
			}

		}else{

			Console.WriteLine("Selected seats: None");
		}
	}


	public static void ChangeSeatLegenda(){
		Console.BackgroundColor = ConsoleColor.Yellow; Console.Write("\n\n[N]".PadLeft(3)); Console.ResetColor(); Console.Write($" = Normal seat  ({DisplayRoom.NORMAL_SEAT_PRICE} euro)\n");
		Console.BackgroundColor = ConsoleColor.Blue; Console.Write("[D]"); Console.ResetColor(); Console.Write($" = Deluxe seat  ({DisplayRoom.DELUXE_SEAT_PRICE} euro)\n");
		Console.BackgroundColor = ConsoleColor.Red; Console.Write("[P]"); Console.ResetColor(); Console.Write($" = Premium seat  ({DisplayRoom.PREMIUM_SEAT_PRICE} euro)\n");
		Console.BackgroundColor = ConsoleColor.DarkGray; Console.Write("[R]"); Console.ResetColor(); Console.Write(" = Reserved seat  \n");
		Console.BackgroundColor = ConsoleColor.Magenta; Console.Write("[S]"); Console.ResetColor(); Console.Write(" = Selected seat  \n");
		Console.BackgroundColor = ConsoleColor.Black; Console.Write("---"); Console.ResetColor(); Console.Write(" = Unselectable place  \n");
		Console.Write("_____"); Console.Write(" = Screen  \n");
		Console.Write("Use the arrow keys to navigate the seats\n");
		Console.Write("Press Enter to change seat type\n");
		Console.Write("Press Backspace when you are finished and wish to go to the main menu\n");
	}

	public static void changeSeattype(string fileNM){

		List<Seating> seatinglist = JsonAccess.ReadFromJson<Seating>(fileNM);

		Seating ?seating = seatinglist[0];

		int selectedPositionCol = 0;
		int selectedPositionRow = 0;

		Tuple<int,int> lastPos = new Tuple<int, int>(selectedPositionRow,selectedPositionCol);

		try{
			while(!Console.KeyAvailable){

				if (seating == null)
				{
					Console.WriteLine("Failed to load seating arrangement from JSON.");
					break;
				}

				DisplayRoom.getColCount(seating);
				
				DisplayRoom.SetColor(selectedPositionRow, selectedPositionCol, seating);

				ChangeSeatLegenda();
				
				try{
					switch(Console.ReadKey(true).Key){
						case ConsoleKey.UpArrow:
							selectedPositionRow = Math.Max(0, selectedPositionRow - 1);
						break;
						case ConsoleKey.DownArrow:
							selectedPositionRow = Math.Min(seating!.Rows - 1, selectedPositionRow + 1);
						break;
						case ConsoleKey.LeftArrow:
							selectedPositionCol = Math.Max(0, selectedPositionCol - 1);
						break;
						case ConsoleKey.RightArrow:
							selectedPositionCol = Math.Min(seating!.Columns - 1, selectedPositionCol + 1);
						break;
						case ConsoleKey.Enter:

							var seat = seating!.SeatingArrangement[selectedPositionRow, selectedPositionCol][0];

							seat.Type = (SeatType)(((int)seat.Type + 1) % Enum.GetValues(typeof(SeatType)).Length);
							DisplayRoom.changeSeatprice(seat);

						break;
						case ConsoleKey.Backspace:

							MenuUtils.displayLoggedinAdminMenu();

						break;
					}
				}catch(Exception ex ){
					Console.WriteLine(ex.Message);
				}

				List<Seating> UploadSeating = new(){seating!};

				JsonAccess.UploadToJson(UploadSeating, fileNM);
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error displaying seating: {ex.Message} \n {ex.Data} \n {ex.GetBaseException()} \n {ex.GetObjectData} \n {ex.StackTrace}");
		}
	}

}