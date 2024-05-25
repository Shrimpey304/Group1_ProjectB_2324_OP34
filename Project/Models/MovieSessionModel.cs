namespace Cinema;

public class MovieSessionModel
{
	public static int sID = 1;
	public int sessionID;
	public DateTime StartTime;
	public DateTime EndTime;
	public int MovieID;
	public int RoomID;
	
	// public DisplayRoom Room;
	
	public MovieSessionModel(DateTime start, DateTime end, int movieID, int roomID)
	{
		StartTime = start;
		EndTime = end;
		MovieID = movieID;
		sessionID = sID ++;
		RoomID = roomID;
	}

}