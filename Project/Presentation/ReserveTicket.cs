namespace Cinema;

public class ReserveTicket{

	public static void ReserveProcess(){

		int selectedMovieID = MovieLogic.ListAllMovies();
		Console.WriteLine("\n\n");
		MovieSessionModel selectedSession = MovieSessionLogic.ListSessions(selectedMovieID);
		List<MovieSessionModel> session = JsonAccess.ReadFromJson<MovieSessionModel>($"DataStorage/Sessions.json");
		List<Tuple<int, int>> selectedSeating = DisplayRoom.SelectSeating(selectedSession);
		if (selectedSeating != null){
			Thread.Sleep(1000);
			if(AccountsLogic.CurrentAccount != null){
				AccountsLogic instAL = new();
				instAL.UpdateList(AccountsLogic.CurrentAccount);
				MenuUtils.displaySnackOption();
				Ticket newticket = new(session[0], selectedSeating,SnackMenuLogic.TotalCost,SnackMenuLogic.OrderedSnacks,AccountsLogic.CurrentAccount.Id);
				Console.WriteLine($"movie: {newticket.moviesession.MovieID} \nRoom: {newticket.moviesession.RoomID} Seats: {newticket.ReservedSeats} ");
				AccountsLogic.CurrentAccount.TicketList.Add(newticket);
				TicketLogic.AddReservation(newticket);
			}else{
				Console.WriteLine("You are not logged in");
				Thread.Sleep(2000);
			}
		}
	}
}