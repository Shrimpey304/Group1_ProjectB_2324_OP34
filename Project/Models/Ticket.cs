namespace Cinema;

public class Ticket{

    //User class voor userdata nodig
    //Movie Movie class voor moviedata
    DateOnly DateOfMovie;
    DateTime TimeOfMovie;
    List<Tuple<int,int>> ReservedSeats;

    public Ticket(DateOnly dateofmovie, DateTime timeofmovie, List<Tuple<int,int>> reservedseats){

        DateOfMovie = dateofmovie;
        TimeOfMovie = timeofmovie;
        ReservedSeats = reservedseats;

    }

}