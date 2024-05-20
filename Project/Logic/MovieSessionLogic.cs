namespace Cinema;

public class MovieSessionLogic
{

	public static MovieSessionModel ListSessions(int UserInput)
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
			Console.WriteLine("Upcoming sessions for this movie:");
			List<MovieSessionModel> usableSessions = new();

			Console.WriteLine($" _________________________________________________________________");
			Console.WriteLine($"| {"Session".PadRight(7)} | {"Start".PadRight(25)} | {"End".PadRight(25)} |");
			Console.WriteLine($"|---------+---------------------------+---------------------------|");

			foreach (MovieSessionModel session in SessionList)
			{
				if (session.MovieID == UserInput)
				{
					Console.WriteLine($"| {Convert.ToString(Counter).PadRight(7)} | {Convert.ToString(session.StartTime).PadRight(25)} | End: {Convert.ToString(session.EndTime).PadRight(20)} |");
					usableSessions.Add(session);
					Counter++;
				}
			}

			Console.WriteLine($" -----------------------------------------------------------------");
			
			Console.WriteLine("\nPlease select a session by typing the session ID.");

			string ?inp = Console.ReadLine();
			int intinp = Convert.ToInt32(inp);

			return usableSessions[intinp-1];
		}
		else
		{
			Console.WriteLine("There are currently no sessions planned for this movie.\nPerhaps a different movie piques your interest.");
			return null!;
		}
	}

	public static MovieSessionModel getSessionByID(int movieID){

		List<MovieSessionModel> _sessions = JsonAccess.ReadFromJson<MovieSessionModel>("DataStorage/Sessions.json");

		return _sessions.FirstOrDefault(a => a.sessionID == movieID)!;
	}
}