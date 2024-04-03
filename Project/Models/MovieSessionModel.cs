namespace Cinema;

public class MovieSessionModel
{
	public static int sID = 1;
	public int sessionID;
	public DateTime StartTime;
	public DateTime EndTime;
	public int MovieID;
	
	// public DisplayRoom Room;
	
	public MovieSessionModel(DateTime start, DateTime end, int movieID)
	{
		StartTime = start;
		EndTime = end;
		MovieID = movieID;
		sessionID = sID ++;
	}

	// public static void MakeSession(DateTime start, DateTime end){

	// 	MovieSession session = new(start, end);
	// 	List<MovieSession> sessionToUpload = new(){session};
	// 	const string filePath = "DataStorage/Sessions.json";
		
	// 	JsonAccess.UploadToJson(sessionToUpload, filePath);

	// }


}