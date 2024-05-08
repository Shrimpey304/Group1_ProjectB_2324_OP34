namespace Cinema;

public class TicketLogic{

	private const string filePathReservations = @"DataStorage\Reservation.json";
	private static List<Ticket> _reservations = JsonAccess.ReadFromJson<Ticket>(filePathReservations) ?? new List<Ticket>();

	public static MovieSessionModel selectedSession;
	public static List<Tuple<int, int>> selectedSeating;
	public static void ReserveTicket()
	{
		int selectedMovieID = MovieLogic.ListAllMovies();
		Console.WriteLine("\n\n");
		selectedSession = MovieSessionLogic.ListSessions(selectedMovieID);
		selectedSeating = DisplayRoom.SelectSeating(selectedSession);
		MenuUtils.displaySnackOption();

	}
	public static void AddReservation()
	{
		List<MovieSessionModel> session = JsonAccess.ReadFromJson<MovieSessionModel>($"DataStorage/Sessions.json");
		Ticket newticket = new(session[0],selectedSeating,SnackMenuLogic.TotalCost,SnackMenuLogic.OrderedSnacks,AccountsLogic.CurrentAccount.Id);
		Console.WriteLine($"movie: {newticket.moviesession.MovieID} \nRoom: {newticket.moviesession.RoomID} Seats: {newticket.ReservedSeats} ");
		AccountsLogic.CurrentAccount.TicketList.Add(newticket);
		_reservations.Add(newticket);
		JsonAccess.UploadToJson(_reservations,filePathReservations);
		foreach (Ticket ticket1  in _reservations)
		{
			Console.WriteLine("test werkt");
		}
	}
	


}