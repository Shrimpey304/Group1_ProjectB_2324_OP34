namespace Cinema;

public class MovieSessionLogic
{
	public static void ListSessions(int UserInput)
	{
		List<MovieSessionModel> SessionList = JsonAccess.ReadFromJson<MovieSessionModel>("DataStorage/Sessions.json");
		bool HasSessions = false;
		
		foreach (MovieSessionModel session in SessionList)
		{
			if (session.MovieID == UserInput)
			{
				HasSessions = true;
			}
		}

		if (HasSessions){
			int Counter = 1;
			foreach (MovieSessionModel session in SessionList)
			{
				if (session.MovieID == UserInput)
				{
					Console.WriteLine($"Session {Counter++}:\nStart: {session.StartTime}\nEnd: {session.EndTime}\n");
				}
			}
		}
		else
		{
			Console.WriteLine("There are currently no sessions planned for this movie.\nPerhaps a different movie piques your interest.");
		}
	}
}