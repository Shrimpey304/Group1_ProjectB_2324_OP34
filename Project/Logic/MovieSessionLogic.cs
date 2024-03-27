namespace Cinema;

public class MovieSessionLogic
{
	public static void ListSessions(int UserInput)
	{
		List<MovieSessionModel> SessionList = JsonAccess.ReadFromJson<MovieSessionModel>("DataStorage/Sessions.json");
		int Counter = 1;
		
		foreach (MovieSessionModel session in SessionList)
		{
			if (session.Movie.movieID == UserInput)
			{
				Console.WriteLine($"Session {Counter++}:\nStart: {session.StartTime}\nEnd: {session.EndTime}\n");
			}
		}
	}
}