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
		Console.WriteLine(HasSessions);
		if (HasSessions){
			int Counter = 0;
			foreach (MovieSessionModel session in SessionList)
			{
				Console.WriteLine(Counter);
				if (session.MovieID == UserInput)
				{
					Console.WriteLine(session.MovieID);
					Console.WriteLine($"Session {Counter++}:\nStart: {session.StartTime}\nEnd: {session.EndTime}\n");
					Thread.Sleep(2000);
					return session;
				}
				Thread.Sleep(2000);
			}
		}
		else
		{
			Console.WriteLine("There are currently no sessions planned for this movie.\nPerhaps a different movie piques your interest.");
			Thread.Sleep(2000);
			return null;
		}
		Thread.Sleep(2000);
		return null;
	}
}