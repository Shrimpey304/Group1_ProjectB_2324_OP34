using System.Linq.Expressions;

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

			Console.WriteLine($" _________________________________________________________________________");
			Console.WriteLine($"| {"Session".PadRight(7)} | {"Start".PadRight(25)} | {"End".PadRight(25)} | Room: |");
			Console.WriteLine($"|---------+---------------------------+---------------------------+-------|");

			foreach (MovieSessionModel session in SessionList)
			{
				if (session.MovieID == UserInput)
				{
					Console.WriteLine($"| {Convert.ToString(Counter).PadRight(7)} | {Convert.ToString(session.StartTime).PadRight(25)} | End: {Convert.ToString(session.EndTime).PadRight(20)} | {Convert.ToString(session.RoomID).PadRight(5)} |");
					usableSessions.Add(session);
					Counter++;
				}
			}

			Console.WriteLine($" ------------------------------------------------------------------------");
			
			Console.WriteLine("\nPlease select a session by typing the session ID and pressing enter.");

		
			int intinp;
			while (true)
			{
				Console.WriteLine("\nPlease select a session by typing the session ID and pressing enter.");
				string inp = Console.ReadLine();

				// Try to convert the input to an integer
				if (int.TryParse(inp, out intinp))
				{
					// Check if the integer is within the range of the list indices
					if (intinp >= 1 && intinp <= usableSessions.Count)
					{
						// Break the loop if the input is valid
						break;
					}
					else
					{
						Console.WriteLine("The input is out of range. Please enter a valid session ID.");
					}
				}
				else
				{
					Console.WriteLine("Invalid input. Please enter a numeric session ID.");
				}
			}

        	// Return the selected session
        	return usableSessions[intinp - 1];
		}
		else
		{
			Console.WriteLine("There are currently no sessions planned for this movie.\nPerhaps a different movie piques your interest.");
			return null!;
		}
	}

	public static void ListSessionsNoReservation(int UserInput)
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

			Console.WriteLine($" _________________________________________________________________________");
			Console.WriteLine($"| {"Session".PadRight(7)} | {"Start".PadRight(25)} | {"End".PadRight(25)} | Room: |");
			Console.WriteLine($"|---------+---------------------------+---------------------------+-------|");

			foreach (MovieSessionModel session in SessionList)
			{
				if (session.MovieID == UserInput)
				{
					Console.WriteLine($"| {Convert.ToString(Counter).PadRight(7)} | {Convert.ToString(session.StartTime).PadRight(25)} | End: {Convert.ToString(session.EndTime).PadRight(20)} | {Convert.ToString(session.RoomID).PadRight(5)} |");
					usableSessions.Add(session);
					Counter++;
				}
			}

			Console.WriteLine($" ------------------------------------------------------------------------");
			

		}
		else
		{
			Console.WriteLine("There are currently no sessions planned for this movie.\nPerhaps a different movie piques your interest.");
		}
	}

	public static MovieSessionModel getSessionByID(int movieID){

		List<MovieSessionModel> _sessions = JsonAccess.ReadFromJson<MovieSessionModel>("DataStorage/Sessions.json");

		return _sessions.FirstOrDefault(a => a.sessionID == movieID)!;
	}
}