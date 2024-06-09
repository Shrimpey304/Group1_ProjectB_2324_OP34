namespace Cinema;
public class TicketLogic
{
	private const string filePathReservations = @"DataStorage\Reservation.json";

	private static List<Ticket> _reservations = JsonAccess.ReadFromJson<Ticket>(filePathReservations) ?? new List<Ticket>();

	public static MovieSessionModel? selectedSession;
	public static List<Tuple<int, int>>? selectedSeating;
	public static double totalSeatPrice;

	public static void ReserveTicket()
	{
		int selectedId = MovieUI.ListAllMovies();
		Console.WriteLine("\n\n");
		selectedSession = MovieSessionUI.ListSessions(selectedId);
		selectedSeating = DisplayRoomUI.SelectSeating(selectedSession);
		totalSeatPrice = DisplayRoom.getSeatPricing(selectedSeating, selectedSession);
		MenuUtils.displaySnackOption();
	}

	public static void AddReservation()
	{
		List<Tuple<string, int>> orderedSnacks = new();
		
		if (SnackMenuLogic.OrderedSnacks != null)
		{
			foreach (Tuple<string, int> item in SnackMenuLogic.OrderedSnacks)
			{
				orderedSnacks.Add(item);
			}
		}

		int newTicketID = GetNextTicketID();

		Ticket newTicket = new Ticket(
			newTicketID,
			selectedSession!.sessionID, 
			selectedSeating!, 
			totalSeatPrice + SnackMenuLogic.TotalCost, 
			orderedSnacks,
			AccountsLogic.CurrentAccount!.Id, 
			AccountsLogic.CurrentAccount!.Id
		);

		int currentRoomID = MovieLogic.getSession(newTicket.SessionID).RoomID;
		
		Console.WriteLine($"movie: {newTicket.SessionID}\nRoom: {currentRoomID} Seats: {newTicket.ReservedSeats} ");
		
		_reservations.Add(newTicket);
		JsonAccess.UploadToJson(_reservations, filePathReservations);
		ConfirmationUI.ShowConfirmation(newTicket);
		SnackMenuLogic.FinishOrder = false;
		Console.WriteLine("Press any key to return to the main menu");
		Console.ReadKey();
		SnackMenuLogic.OrderedSnacks.Clear();
		MenuUtils.displayLoggedinMenu();
	}

	public static void CancelReservation()
	{
		Console.WriteLine("Your tickets:");
		AccountsLogic.CancelTickets();

		Console.WriteLine("Enter the Ticket ID to cancel (leave blank if you don't want to cancel any reservation): ");
		string input = Console.ReadLine()!;
		if (string.IsNullOrEmpty(input))
		{
			Console.WriteLine("Cancellation cancelled.");
			return;
		}


		if (int.TryParse(input, out int reservationId))
		{
			Ticket reservationToCancel = _reservations.FirstOrDefault(r => r.TicketID == reservationId)!;

			if (reservationToCancel != null)
			{
				// Remove the SessionID from the corresponding cinema room JSON file
				RemoveSessionFromCinemaRoom(reservationToCancel.SessionID);

				_reservations.Remove(reservationToCancel);
				JsonAccess.UploadToJson(_reservations, filePathReservations);
				Console.WriteLine($"Reservation with ID {reservationId} cancelled successfully.");
				CancellationUI.ShowCancellation(reservationToCancel);
			}
			else
			{
				Console.WriteLine("Reservation not found.");
			}
		}
		else
		{
			Console.WriteLine("Invalid reservation ID.");
		}
	}
	public static void ReserveFilteredTicket(int selectedId){
		
		Console.WriteLine("\n\n");
		selectedSession = MovieSessionUI.ListSessions(selectedId);
		selectedSeating = DisplayRoomUI.SelectSeating(selectedSession);
		totalSeatPrice = DisplayRoom.getSeatPricing(selectedSeating, selectedSession);
		MenuUtils.displaySnackOption();
	}

	private static void RemoveSessionFromCinemaRoom(int sessionID)
	{
		// Get the file directory for the cinema room JSON files
		List<string> cinemaRoomFiles = DisplayRoom.getFileDir();

		foreach (string file in cinemaRoomFiles)
		{
			// Load the JSON data
			List<Seating> seatingJson = JsonAccess.ReadFromJson<Seating>(file);

			// Find the session ID and remove it from the reservedInSessionID list
			foreach (Seating seating in seatingJson)
			{
				for (int i = 0; i < seating.Rows; i++)
				{
					for (int j = 0; j < seating.Columns; j++)
					{
						SeatInfo seat = seating.SeatingArrangement[i, j][0];
						if (seat.reservedInSessionID.Contains(sessionID))
						{
							seat.reservedInSessionID.Remove(sessionID);
						}
					}
				}
			}

			// Save the modified data back to the JSON file
			JsonAccess.UploadToJson(seatingJson, file);
		}
	}


	private static int GetNextTicketID()
	{
		if (_reservations.Count == 0)
		{
			return 1; // Start with ID 1 if there are no existing reservations
		}
		else
		{
			int maxID = _reservations.Max(t => t.TicketID);
			return maxID + 1;
		}
	}
	public static List<Ticket> GetReservationsBySession(int sessionID)
	{
		// Read all reservations from the JSON file
		List<Ticket> allReservations = JsonAccess.ReadFromJson<Ticket>(filePathReservations) ?? new List<Ticket>();

		// Filter reservations by session ID
		List<Ticket> reservationsForSession = allReservations.Where(r => r.SessionID == sessionID).ToList();

		return reservationsForSession;
	}

	public static void CancelReservations(List<Ticket> reservations)
	{
		foreach (var reservationToCancel in reservations)
		{
			// Remove the SessionID from the corresponding cinema room JSON file
			RemoveSessionFromCinemaRoom(reservationToCancel.SessionID);

			// Remove the reservation from the list of reservations
			_reservations.Remove(reservationToCancel);

			// Upload the updated list of reservations to JSON
			JsonAccess.UploadToJson(_reservations, filePathReservations);

			// Show cancellation and send email
			CancellationUI.ShowCancellation(reservationToCancel);
		}
	}

}

