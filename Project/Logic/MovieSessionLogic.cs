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
			MovieSessionModel chosenSession = null;
			Console.WriteLine("Upcoming sessions for this movie:");
			// reservation issue ----------------------------------------------------------------------------------------------
			foreach (MovieSessionModel session in SessionList)
			{
				if (session.MovieID == UserInput)
				{
					Console.WriteLine($"Session: {Counter++} | Start: {session.StartTime} | End: {session.EndTime}");
					
				}
			}
			System.Console.WriteLine("\nPlease select a session by typing the session ID.");
			string ?inp = Console.ReadLine();
			int intinp = Convert.ToInt32(inp);
			foreach (MovieSessionModel session in SessionList){
				if(session.sessionID == intinp){
					chosenSession = session;
				}
			}
			return chosenSession;
			// reservation issue ----------------------------------------------------------------------------------------------
		}
		else
		{
			Console.WriteLine("There are currently no sessions planned for this movie.\nPerhaps a different movie piques your interest.");
			return null;
		}
	}
}