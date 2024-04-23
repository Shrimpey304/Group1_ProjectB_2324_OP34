namespace Cinema;

public class ReserveTicket{

    public static void ReserveProcess(){

        int selectedMovieID = MovieLogic.ListAllMovies();
        Console.WriteLine("\n\n");
        MovieSessionModel selectedSession = MovieSessionLogic.ListSessions(selectedMovieID);
        List<MovieSessionModel> session = JsonAccess.ReadFromJson<MovieSessionModel>($"DataStorage/Sessions.json");
        List<Tuple<int, int>> selectedSeating = DisplayRoom.SelectSeating(selectedSession);
        if (selectedSeating != null){
            Ticket newticket = new(session[0], selectedSeating);
            if(AccountsLogic.CurrentAccount != null){
                AccountsLogic instAL = new();
                AccountsLogic.CurrentAccount.TicketList.Add(newticket);
                instAL.UpdateList(AccountsLogic.CurrentAccount);
            }else{
                Console.WriteLine("You are not logged in");
                Thread.Sleep(2000);
            }
        }
    }
}