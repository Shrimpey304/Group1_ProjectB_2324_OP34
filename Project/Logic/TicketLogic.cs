namespace Cinema;

public class TicketLogic{

	private const string filePathReservations = @"DataStorage\Reservation.json";
	private static List<Ticket> _reservations = JsonAccess.ReadFromJson<Ticket>(filePathReservations) ?? new List<Ticket>();

	public static MovieSessionModel ?selectedSession;
	public static List<Tuple<int, int>> ?selectedSeating;
	public static double totalSeatPrice;

	public static void ReserveTicket()
	{
		int selectedMovieID = MovieUI.ListAllMovies();
		Console.WriteLine("\n\n");
		selectedSession = MovieSessionUI.ListSessions(selectedMovieID);
		selectedSeating = DisplayRoomUI.SelectSeating(selectedSession);
		totalSeatPrice = DisplayRoom.getSeatPricing(selectedSeating, selectedSession);
		MenuUtils.displaySnackOption();
	}

	public static void AddReservation()
	{
		// List<MovieSessionModel> session = JsonAccess.ReadFromJson<MovieSessionModel>($"DataStorage/Sessions.json");
		
		Ticket newticket = new(selectedSession!.sessionID, selectedSeating!, totalSeatPrice + SnackMenuLogic.TotalCost, SnackMenuLogic.OrderedSnacks, AccountsLogic.CurrentAccount!.Id, AccountsLogic.CurrentAccount!.Id);
		
		int currentRoomID = MovieLogic.getSession(newticket.sessionID).RoomID;
		
		// foreach (Ticket ticket in _reservations.Where(t => t.sessionID == newticket.sessionID))
		// {
		// 	currentRoomID = JsonAccess.ReadFromJson<MovieSessionModel>("DataStorage/Sessions.json").Where(s => s.sessionID == ticket.sessionID).First().RoomID;
		// }
		
		Console.WriteLine($"movie: {newticket.sessionID}\nRoom: {currentRoomID} Seats: {newticket.ReservedSeats} ");
		
		_reservations.Add(newticket);
		JsonAccess.UploadToJson(_reservations,filePathReservations);
		ConfirmationUI.ShowConfirmation(newticket);
		SnackMenuLogic.FinishOrder = false;
		Console.WriteLine("Press any key to return to the main menu");
		Console.ReadKey();
		MenuUtils.displayLoggedinMenu();
	}
}