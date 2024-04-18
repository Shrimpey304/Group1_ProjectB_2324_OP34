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
			MovieSessionModel chosenSesh = null;
			foreach (MovieSessionModel session in SessionList)
			{
				if (session.MovieID == UserInput)
				{
					Console.WriteLine($"Session: {Counter++} | Start: {session.StartTime} | End: {session.EndTime}");
					
				}
			}
			string ?inp = Console.ReadLine();
			int intinp = Convert.ToInt32(inp);
			foreach (MovieSessionModel session in SessionList){
				if(session.sessionID == intinp){
					chosenSesh = session;
				}
			}
			return chosenSesh;
		}
		else
		{
			Console.WriteLine("There are currently no sessions planned for this movie.\nPerhaps a different movie piques your interest.");
			return null;
		}
	}
}