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
	private static int nextId = 1;
	

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
				nextId = movies[movies.Count - 1].Id + 1;
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
                nextId = movies.Max(m => m.Id) + 1;
            }
            else
            {
                nextId = 1;
            }

            movie.Id = nextId++;
            movies.Add(movie);
            SaveMovies();
        }

	private static void SaveMovies()
	{
		string json = JsonConvert.SerializeObject(movies, Formatting.Indented);
		File.WriteAllText(filePathMovies, json);
	}

	public static MovieModel GetMovieByID(int Id)
	{
		foreach (var movie in movies)
		{
			if (movie.Id == Id)
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
			if (movies[i].Id == updatedMovie.Id)
			{
				movies[i] = updatedMovie;
				JsonAccess.UploadToJson(movies, filePathMovies);
				break;
			}
		}
	}

	public static bool DeleteMovie(int Id)
	{
		for (int i = 0; i < movies.Count; i++)
		{
			if (movies[i].Id == Id)
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
		List<MovieSessionModel> sessionList = JsonAccess.ReadFromJson<MovieSessionModel>(filePathSessions);

		int lastSessionID = sessionList.Any() ? sessionList.Max(s => s.sessionID) : 0;
		session.sessionID = lastSessionID + 1;

		sessionList.Add(session);
		JsonAccess.UploadToJson(sessionList, filePathSessions);
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

	public static void UpdateMovieSession(int sessionID, MovieSessionModel updatedSession)
	{
		List<MovieSessionModel> sessions = JsonAccess.ReadFromJson<MovieSessionModel>(filePathSessions);

		foreach (var session in sessions)
		{
			if (session.sessionID == sessionID)
			{
				// Update session properties
				session.StartTime = updatedSession.StartTime;
				session.EndTime = updatedSession.EndTime;
				session.Id = updatedSession.Id;
				session.RoomID = updatedSession.RoomID;

				// Save updated sessions to file
				JsonAccess.UploadToJson(sessions, filePathSessions);
				return;
			}
		}

		Console.WriteLine($"Session with ID {sessionID} not found.");
	}


	public static bool DeleteMovieSession(int sessionID)
	{
		var sessions = JsonAccess.ReadFromJson<MovieSessionModel>(filePathSessions);
		for (int i = 0; i < sessions.Count; i++)
		{
			if (sessions[i].sessionID == sessionID)
			{
				sessions.RemoveAt(i);
				JsonAccess.UploadToJson(sessions, filePathSessions);
				return true;
			}
		}
		return false;
	}

	
	public static string FindMovie(int Id)
	{
		var movies = JsonAccess.ReadFromJson<MovieModel>(filePathMovies);
		foreach(MovieModel movie in movies)
		{
			if (movie.Id == Id)
			{
				return movie.Title;
			}
		}
		return "";
	}  

	
	public static void movieGenreFilter(){

		Console.Clear();

		List<MovieModel> CurrentMovies = JsonAccess.ReadFromJson<MovieModel>(filePathMovies);
		List<MovieModel> SelectedMovies = new();

		DisplayHeaderUI.HeaderMain();
		Console.WriteLine("\n---------------------------------------------------------------------------\n");
		Console.WriteLine("Enter what genre you want to filter by");
		Console.Write(">>>");
		string filter = Console.ReadLine()!;

		foreach(MovieModel movie in CurrentMovies){

			if(movie.GenreName.Contains(filter)){

				SelectedMovies.Add(movie);

			}
		}

		if (SelectedMovies.Count != 0){
			MovieUI.ShowFilteredMovies(SelectedMovies);
		}
		
		MenuUtils.displayLoggedinMenu();

	}


	public static void movieNameFilter(){

		Console.Clear();

		List<MovieModel> CurrentMovies = JsonAccess.ReadFromJson<MovieModel>(filePathMovies);
		List<MovieModel> SelectedMovies = new();

		DisplayHeaderUI.HeaderMain();
		Console.WriteLine("\n---------------------------------------------------------------------------\n");
		Console.WriteLine("enter what you want to filter by");
		Console.Write(">>>");
		string filter = Console.ReadLine()!;

		foreach(MovieModel movie in CurrentMovies){

			if(movie.Title.ToLower().Contains(filter.ToLower())){

				SelectedMovies.Add(movie);

			}
		}

		if (SelectedMovies.Count != 0){
			MovieUI.ShowFilteredMovies(SelectedMovies);
		}
		
		MenuUtils.displayLoggedinMenu();

	}

	public static void DisplayMovieDetails(MovieModel movie)
	{
		Console.WriteLine($"Title: {movie.Title}");
		Console.WriteLine($"Age Restriction: {movie.AgeRestriction}");
		Console.WriteLine($"Description: {movie.Description}");
		Console.WriteLine($"Genre: {movie.GenreName}");
	}

	public static MovieSessionModel GetSession(int sessionID)
	{
		var sessions = JsonAccess.ReadFromJson<MovieSessionModel>(filePathSessions);
		foreach (var session in sessions)
		{
			if (session.sessionID == sessionID)
			{
				return session;
			}
		}
		return null!;
	}


}