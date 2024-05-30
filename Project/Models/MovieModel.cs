namespace Cinema;

public class MovieModel
{
	public string Title;
	public static int mID = 1;
	public int MovieID;
	public string Description;
	public string GenreName;
	public int AgeRestriction;

	public MovieModel(string title, int ageRest, string genreName, string description) 
	{
		Title = title;
		Description = description;
		AgeRestriction = ageRest;
		GenreName = genreName;
		MovieID = mID ++;
	}

	public void DisplayAssociatedSessions(MovieSessionModel[] sessions)
	{
		Console.WriteLine($"Sessions associated with movie '{Title}':");
		foreach (var session in sessions)
		{
			Console.WriteLine($"Room ID: {session.RoomID}");
			Console.WriteLine($"Start Time: {session.StartTime}");
			Console.WriteLine($"End Time: {session.EndTime}");
			Console.WriteLine();
		}
	}
	public static void DisplayMovieDetails(MovieModel movie)
        {
            Console.WriteLine($"Title: {movie.Title}");
            Console.WriteLine($"Age Restriction: {movie.AgeRestriction}");
            Console.WriteLine($"Description: {movie.Description}");
            Console.WriteLine($"Genre: {movie.GenreName}");
        }
	
}

