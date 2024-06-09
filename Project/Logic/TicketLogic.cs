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
		List<string> cinemaRoomFiles = DisplayRoom.getFileDir();

		foreach (string file in cinemaRoomFiles)
		{
			List<Seating> seatingJson = JsonAccess.ReadFromJson<Seating>(file);

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

			JsonAccess.UploadToJson(seatingJson, file);
		}
	}


	private static int GetNextTicketID()
	{
		if (_reservations.Count == 0)
		{
			return 1;
		}
		else
		{
			int maxID = _reservations.Max(t => t.TicketID);
			return maxID + 1;
		}
	}
	public static List<Ticket> GetReservationsBySession(int sessionID)
	{
		List<Ticket> allReservations = JsonAccess.ReadFromJson<Ticket>(filePathReservations) ?? new List<Ticket>();

		List<Ticket> reservationsForSession = allReservations.Where(r => r.SessionID == sessionID).ToList();

		return reservationsForSession;
	}

	public static void CancelReservations(List<Ticket> reservations)
	{
		foreach (var reservationToCancel in reservations)
		{
			RemoveSessionFromCinemaRoom(reservationToCancel.SessionID);

			_reservations.Remove(reservationToCancel);

			JsonAccess.UploadToJson(_reservations, filePathReservations);

			CancellationUI.ShowCancellation(reservationToCancel);
		}
	}

}

