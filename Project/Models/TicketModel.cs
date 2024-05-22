namespace Cinema;

public class Ticket{

	//User class voor userdata nodig
	//Movie Movie class voor moviedata
	public int TicketID { get; set; }
	public List<Ticket> TicketList { get; set; }
	public MovieSessionModel moviesession;
	public List<Tuple<int,int>> ReservedSeats;
	
	public double Totalprice;
	
	public static int UserID;
	public Ticket(MovieSessionModel session, List<Tuple<int,int>> reservedseats, double totalprice, List<Tuple<string, int>> orderedsnacks,int userID){

		moviesession = session;
		ReservedSeats = reservedseats;
		Totalprice = totalprice;
		SnackMenuLogic.OrderedSnacks = orderedsnacks ?? new List<Tuple<string, int>>();
		UserID = userID;
	}

}