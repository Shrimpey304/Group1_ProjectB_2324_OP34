namespace Cinema;
public class MovieLogic
{
	private int _ageRestriction;
	public int AgeRestriction
	{
		get => _ageRestriction;
		set
		{
			if (value < AgeRestriction)
			{
				_ageRestriction = value;
			}
		}
	}

	public static bool IsDigitsOnly(string str)
{
	foreach (char c in str)
	{
		if (c < '0' || c > '9')
			return false;
	}

	return true;
}

	public static void AddMovie(MovieModel movie)
	{
		List<MovieModel> MovieList = JsonAccess.ReadFromJson<MovieModel>("Movies.json");

		MovieList.Add(movie);

		JsonAccess.UploadToJson(MovieList, "Movies.Json");
	}

	const string filePathMovies = "DataStorage/Movies.json";

	public static void ListAllMovies()
	{
		List<MovieModel> MovieList = JsonAccess.ReadFromJson<MovieModel>(filePathMovies);
		foreach (MovieModel movie in MovieList)
		{
			Console.WriteLine("[{0}]\nTitle: {1}\nAge Restriction: {2}\nDescription: {3}\nGenre: {4}\n\n",movie.movieID, movie.Title, movie.AgeRestriction, movie.Description, movie.GenreName);

		}
		Console.Write("Type the ID of a movie to see it's upcoming sessions.\n  >>> ");
		string UserInput;
		do
		{
			UserInput = Console.ReadLine();
		}while ((UserInput == null) || (IsDigitsOnly(UserInput) == false) || (UserInput == ""));
		
		int UserInputInt = Convert.ToInt32(UserInput);
		foreach (MovieModel movie in MovieList)
		{
			if (UserInputInt == movie.movieID)
			{
				Console.WriteLine("Current selected movie:");
				Console.WriteLine("\nTitle: {0}\nAge Restriction: {1}\nDescription: {2}\nGenre: {3}\n\n", movie.Title, movie.AgeRestriction, movie.Description, movie.GenreName);
				//ListSessions()
			}
		}
	}
}
