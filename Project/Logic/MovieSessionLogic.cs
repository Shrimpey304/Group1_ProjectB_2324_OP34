namespace Cinema;

public class MovieSessionLogic
{
	public static int ListSessions(int UserInput)
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
					return session.sessionID;
				}
				return 0;
			}
		}
		else
		{
			Console.WriteLine("There are currently no sessions planned for this movie.\nPerhaps a different movie piques your interest.");
			return 0;
		}
		return 0;
	}
}