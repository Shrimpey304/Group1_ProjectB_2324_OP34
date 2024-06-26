namespace Cinema;

public class SeatInfo
{
	public int RowID { get; set; }
	public int ColumnID { get; set; }
	public double Price { get; set; }
	public bool inPrereservation { get; set; }
	public List<int> reservedInSessionID = new();
	public SeatType Type { get; set; }
}

public enum SeatType
{
	Normal,
	Deluxe,
	Premium,
	NoSeat
}