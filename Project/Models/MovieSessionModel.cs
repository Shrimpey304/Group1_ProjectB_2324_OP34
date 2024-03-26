namespace Cinema;

public class MovieSession
{
	public static int sID = 1;
	public int sessionID;
	public DateTime StartTime;
	public DateTime EndTime;
	
	// public DisplayRoom Room;
	
	public MovieSession(DateTime start, DateTime end)
	{
		StartTime = start;
		EndTime = end;
		sessionID = sID ++;
	}

	public static void MakeSession(DateTime start, DateTime end){

		MovieSession session = new(start, end);
		List<MovieSession> sessionToUpload = new(){session};
		const string filePath = "DataStorage/Sessions.json";

		SessionsJsonUtils.UploadToJson(sessionToUpload, filePath);

	}


}