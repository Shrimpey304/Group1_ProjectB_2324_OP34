using Newtonsoft.Json;

namespace Cinema;
public class MovieLogic
{
	const string filePathMovies = "DataStorage/Movies.json";
	const string filePathSessions = "DataStorage/Sessions.json";
	private int _ageRestriction;
	public int AgeRestriction
	{
		get => _ageRestriction;
		set
		{
			if (value > AgeRestriction)
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


	private static List<MovieModel> movies;
	private static int nextMovieID = 1;
	

	static MovieLogic()
	{
		InitializeMovies();
	}

	private static void InitializeMovies()
	{
		if (File.Exists(filePathMovies))
		{
			string json = File.ReadAllText(filePathMovies);
			movies = JsonConvert.DeserializeObject<List<MovieModel>>(json)!;
			if (movies != null && movies.Count > 0)
			{
				nextMovieID = movies[movies.Count - 1].MovieID + 1;
			}
			else
			{
				movies = new List<MovieModel>();
			}
		}
		else
		{
			movies = new List<MovieModel>();
		}
	}

	public static void AddMovie(MovieModel movie)
        {
            if (movies.Count > 0)
            {
                nextMovieID = movies.Max(m => m.MovieID) + 1;
            }
            else
            {
                nextMovieID = 1;
            }

            movie.MovieID = nextMovieID++;
            movies.Add(movie);
            SaveMovies();
        }

	private static void SaveMovies()
	{
		string json = JsonConvert.SerializeObject(movies, Formatting.Indented);
		File.WriteAllText(filePathMovies, json);
	}

	public static MovieModel GetMovieByID(int movieID)
	{
		foreach (var movie in movies)
		{
			if (movie.MovieID == movieID)
			{
				return movie;
			}
		}
		return null!;
	}

	public static void UpdateMovie(MovieModel updatedMovie)
	{
		for (int i = 0; i < movies.Count; i++)
		{
			if (movies[i].MovieID == updatedMovie.MovieID)
			{
				movies[i] = updatedMovie;
				JsonAccess.UploadToJson(movies, filePathMovies);
				break;
			}
		}
	}

	public static bool DeleteMovie(int movieID)
	{
		for (int i = 0; i < movies.Count; i++)
		{
			if (movies[i].MovieID == movieID)
			{
				movies.RemoveAt(i);
				JsonAccess.UploadToJson(movies, filePathMovies);
				return true;
			}
		}
		return false;
	}


	public static void AddMovieSession(MovieSessionModel session)
	{
		List<MovieSessionModel> SessionList = JsonAccess.ReadFromJson<MovieSessionModel>(filePathSessions);

		int cnt = 0;
		foreach(MovieSessionModel sesh in SessionList){
			cnt ++;
		}
		session.sessionID = cnt;

		SessionList.Add(session);

		JsonAccess.UploadToJson(SessionList, filePathSessions);
	}

	public static MovieSessionModel getSession(int id){
		
		var sessions = JsonAccess.ReadFromJson<MovieSessionModel>(filePathSessions);
		foreach(MovieSessionModel sesh in sessions){
			if (sesh.sessionID == id){
				return sesh;
			}
		}
		return null!;
	}
	
	public static string FindMovie(int MovieID)
	{
		var movies = JsonAccess.ReadFromJson<MovieModel>(filePathMovies);
		foreach(MovieModel movie in movies)
		{
			if (movie.MovieID == MovieID)
			{
				return movie.Title;
			}
		}
		return "";
	}  

	public static void DisplayMovieDetails(MovieModel movie)
	{
		Console.WriteLine($"Title: {movie.Title}");
		Console.WriteLine($"Age Restriction: {movie.AgeRestriction}");
		Console.WriteLine($"Description: {movie.Description}");
		Console.WriteLine($"Genre: {movie.GenreName}");
	}

}