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
		
		Ticket newticket = new(selectedSession!, selectedSeating!, totalSeatPrice + SnackMenuLogic.TotalCost, SnackMenuLogic.OrderedSnacks, AccountsLogic.CurrentAccount!.Id);
		
		Console.WriteLine($"movie: {newticket.moviesession.MovieID} \nRoom: {newticket.moviesession.RoomID} Seats: {newticket.ReservedSeats} ");
		
		AccountsLogic.CurrentAccount.TicketList.Add(newticket);
		_reservations.Add(newticket);
		JsonAccess.UploadToJson(_reservations,filePathReservations);
		ConfirmationUI.ShowConfirmation(newticket);
		Console.WriteLine("Press any key to return to the main menu");
		Console.ReadKey();
		MenuUtils.displayLoggedinMenu();
	}
}