namespace Cinema;

public class TicketLogic{

    public void DisplayTicketInfo(Ticket ticket){

        DateTime Start = ticket.moviesession.StartTime;
        DateTime End = ticket.moviesession.EndTime;
        int SessionID = ticket.moviesession.sessionID;
        int movieID = ticket.moviesession.MovieID;

        List<MovieSessionModel> sessions = JsonAccess.ReadFromJson<MovieSessionModel>($"DataStorage/Sessions.json");
        MovieSessionModel ?Session = null;
        foreach(var sesh in sessions){
            if (sesh.sessionID == SessionID){
                Session = sesh;
            }
        }

        List<MovieModel> Movies = JsonAccess.ReadFromJson<MovieModel>($"DataStorage/Movies.json");
        MovieModel ?Movie = null;
        foreach(var mov in Movies){
            if (mov.MovieID == movieID){
                Movie = mov;
            }
        }

        Console.WriteLine($"start: {Start} \n end: {End} \n Selected row: {ticket.ReservedSeats[0].Item1} Seats:");
        foreach(Tuple<int,int> seatLoc in ticket.ReservedSeats){

            Console.Write($"{seatLoc.Item2} ");
        }
        Console.WriteLine($"Movie: {Movie.Title} Genre: {Movie.GenreName} Age Restriction: {Movie.AgeRestriction} Room: {Movie.RoomID}");
        Thread.Sleep(12000);
    }

}