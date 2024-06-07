namespace Cinema;

public class MovieSessionModel : IID
{
	public static int sID = 1;
	public int sessionID;
	public DateTime StartTime;
	public DateTime EndTime;
	public int Id { get; set; }
	public int RoomID;
	
	// public DisplayRoom Room;
	
	public MovieSessionModel(DateTime start, DateTime end, int id, int roomID)
	{
		StartTime = start;
		EndTime = end;
		Id = id;
		sessionID = sID ++;
		RoomID = roomID;
	}

}