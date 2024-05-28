namespace Cinema;

public class Ticket{

	//User class voor userdata nodig
	//Movie Movie class voor moviedata
// Properties for Ticket class
	public int TicketID { get; set; }
	public int SessionID { get; private set; }
	public List<Tuple<int, int>> ReservedSeats { get; private set; }
	public double TotalPrice { get; private set; }
	public int AccountID { get; private set; }
	public int UserID { get; private set; }
	public List<Tuple<string, int>> OrderedSnacks { get; private set; }
	
	// Static list of tickets (if this is required for some functionality)
	public static List<Ticket> TicketList { get; } = new List<Ticket>();

	// Constructor for the Ticket class
	public Ticket(int sessionID, List<Tuple<int, int>> reservedSeats, double totalPrice, List<Tuple<string, int>> orderedSnacks, int userID, int accountID)
	{
		SessionID = sessionID;
		ReservedSeats = reservedSeats ?? new List<Tuple<int, int>>();
		TotalPrice = totalPrice;
		OrderedSnacks = orderedSnacks ?? new List<Tuple<string, int>>();
		UserID = userID;
		AccountID = accountID;
	}

}