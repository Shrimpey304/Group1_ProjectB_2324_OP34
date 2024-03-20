namespace Cinema;

public class MovieSession
{
	public string StartTime;
	public string EndTime;
	
	// public DisplayRoom Room;
	
	public MovieSession(string start, string end, string FileName)
	{
		StartTime = start;
		EndTime = end;
		// Room = new DisplayRoom({FileName})
	}
}