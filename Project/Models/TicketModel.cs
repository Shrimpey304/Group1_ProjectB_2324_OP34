namespace Cinema;

public class Ticket{

    //User class voor userdata nodig
    //Movie Movie class voor moviedata
    public MovieSessionModel moviesession;
    public List<Tuple<int,int>> ReservedSeats;

    public Ticket(MovieSessionModel session, List<Tuple<int,int>> reservedseats){

        moviesession = session;
        ReservedSeats = reservedseats;

    }

}