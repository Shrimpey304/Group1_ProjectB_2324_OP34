using Cinema;

public class MovieUI{

	const string filePathMovies = "DataStorage/Movies.json";
	const string filePathSessions = "DataStorage/Sessions.json";

	public static void ShowMovies()
	{
		List<MovieModel> MovieList = JsonAccess.ReadFromJson<MovieModel>(filePathMovies);

		Console.Clear();
		Console.WriteLine($" ____________________________________________________________________________________________");
		Console.WriteLine($"| {"ID".PadRight(4)} | {"Title".PadRight(50)} | {"Age".PadRight(4)}    | {"Genre".PadRight(20)} |");
		Console.WriteLine($"|------+----------------------------------------------------+---------+----------------------|");

		foreach (MovieModel movie in MovieList)
		{
			Console.WriteLine($"| {Convert.ToString(movie.Id).PadRight(4)} | {movie.Title.PadRight(50)} | PG-{Convert.ToString(movie.AgeRestriction).PadRight(4)} | {movie.GenreName.PadRight(20)} |");
		}
		Console.WriteLine($" --------------------------------------------------------------------------------------------");
	}


    public static int ListAllMovies()
	{
		List<MovieModel> MovieList = JsonAccess.ReadFromJson<MovieModel>(filePathMovies);

		string UserInput;
		do
		{
			Console.Clear();
			Console.WriteLine($" ____________________________________________________________________________________________");
			Console.WriteLine($"| {"ID".PadRight(4)} | {"Title".PadRight(50)} | {"Age".PadRight(4)}    | {"Genre".PadRight(20)} |");
			Console.WriteLine($"|------+----------------------------------------------------+---------+----------------------|");

			foreach (MovieModel movie in MovieList)
			{
				// string IdString = Convert.ToString(movie.Id);
				Console.WriteLine($"| {Convert.ToString(movie.Id).PadRight(4)} | {movie.Title.PadRight(50)} | PG-{Convert.ToString(movie.AgeRestriction).PadRight(4)} | {movie.GenreName.PadRight(20)} |");
			}
			Console.WriteLine($" --------------------------------------------------------------------------------------------");
			Console.Write("Type the ID of a movie to see it's upcoming sessions.\n  >>> ");
			UserInput = Console.ReadLine()!;
		}while ((UserInput == null) || (MovieLogic.IsDigitsOnly(UserInput) == false) || (UserInput == ""));
		
		int UserInputInt = Convert.ToInt32(UserInput);  // this is possible, because it is always only numbers (see 2 lines above)
		foreach (MovieModel movie in MovieList)
		{
			if (UserInputInt == movie.Id)
			{
				Console.Clear();
				Console.WriteLine("Current selected movie:");
				// Console.WriteLine("\nTitle: {0}\nAge Restriction: {1}\nDescription: {2}\nGenre: {3}\n\n", movie.Title, movie.AgeRestriction, movie.Description, movie.GenreName);
				Console.WriteLine($"{Convert.ToString(movie.Id)}: {movie.Title} [PG-{Convert.ToString(movie.AgeRestriction)}]");
				// MovieSessionLogic.ListSessions(UserInputInt);
				return UserInputInt;
			}
		}
		return 0;
	}

	public static void ListAllMovies(bool isvoid)
	{
		if(isvoid){
			List<MovieModel> MovieList = JsonAccess.ReadFromJson<MovieModel>(filePathMovies);
			Console.WriteLine($" ____________________________________________________________________________________________");
			Console.WriteLine($"| {"ID".PadRight(4)} | {"Title".PadRight(50)} | {"Age".PadRight(4)}    | {"Genre".PadRight(20)} |");
			Console.WriteLine($"|------+----------------------------------------------------+---------+----------------------|");

			foreach (MovieModel movie in MovieList)
			{
				// string IdString = Convert.ToString(movie.Id);
				Console.WriteLine($"| {Convert.ToString(movie.Id).PadRight(4)} | {movie.Title.PadRight(50)} | PG-{Convert.ToString(movie.AgeRestriction).PadRight(4)} | {movie.GenreName.PadRight(20)} |");
			}
			Console.WriteLine($" --------------------------------------------------------------------------------------------");
			string UserInput;
			do
			{
				Console.Write("Type the ID of a movie to see it's upcoming sessions.\n  >>> ");
				UserInput = Console.ReadLine()!;
			}while ((UserInput == null) || (MovieLogic.IsDigitsOnly(UserInput) == false) || (UserInput == ""));
			
			int UserInputInt = Convert.ToInt32(UserInput);  // this is possible, because it is always only numbers (see 2 lines above)
			foreach (MovieModel movie in MovieList)
			{
				if (UserInputInt == movie.Id)
				{
					Console.Clear();
					Console.WriteLine("Current selected movie:");
					// Console.WriteLine("\nTitle: {0}\nAge Restriction: {1}\nDescription: {2}\nGenre: {3}\n\n", movie.Title, movie.AgeRestriction, movie.Description, movie.GenreName);
					Console.WriteLine($"{Convert.ToString(movie.Id).PadRight(4)} | {movie.Title.PadRight(50)} | PG-{Convert.ToString(movie.AgeRestriction).PadRight(4)} | {movie.GenreName.PadRight(20)}\n");
					MovieSessionUI.ListSessionsNoReservation(UserInputInt);
					// MovieSessionLogic.ListSessions(UserInputInt);
					
				}
			}
		}
	}


	public static void ShowSessions()
	{
		List<MovieSessionModel> SessionList = JsonAccess.ReadFromJson<MovieSessionModel>(filePathSessions);

		Console.Clear();
		Console.WriteLine($" _______________________________________________________________________________________________________________________________");
		Console.WriteLine($"| {"ID".PadRight(4)} | {"Start Time".PadRight(20)} | {"End Time".PadRight(20)} | {"Movie ID".PadRight(8)} | {"Room ID".PadRight(8)} | {"MovieTitle".PadRight(50)} |");
		Console.WriteLine($"|------+----------------------+----------------------+----------+----------+----------------------------------------------------|");

		foreach (MovieSessionModel session in SessionList)
		{
			Console.WriteLine($"| {session.sessionID.ToString().PadRight(4)} | {session.StartTime.ToString("dd-MM-yyyy HH:mm:ss").PadRight(20)} | {session.EndTime.ToString("dd-MM-yyyy HH:mm:ss").PadRight(20)} | {session.Id.ToString().PadRight(8)} | {session.RoomID.ToString().PadRight(8)} | {MovieLogic.GetMovieByID(session.Id).Title.PadRight(50)} |");
		}
		Console.WriteLine($" -------------------------------------------------------------------------------------------------------------------------------");
	}


	public static void ShowFilteredMovies(List<MovieModel> filteredList){
		
		string UserInput;
		do
		{
			Console.Clear();
			Console.WriteLine($" ____________________________________________________________________________________________");
			Console.WriteLine($"| {"ID".PadRight(4)} | {"Title".PadRight(50)} | {"Age".PadRight(4)}    | {"Genre".PadRight(20)} |");
			Console.WriteLine($"|------+----------------------------------------------------+---------+----------------------|");

			int cnt = 0;
			foreach (MovieModel movie in filteredList)
			{
				// string IdString = Convert.ToString(movie.Id);
				Console.WriteLine($"| {Convert.ToString(cnt+1).PadRight(4)} | {movie.Title.PadRight(50)} | PG-{Convert.ToString(movie.AgeRestriction).PadRight(4)} | {movie.GenreName.PadRight(20)} |");
				cnt ++;
			}
			Console.WriteLine($" --------------------------------------------------------------------------------------------");
			Console.Write("Type the ID of a movie to see it's upcoming sessions.\n  >>> ");
			UserInput = Console.ReadLine()!;

		}while ((UserInput == null) || (MovieLogic.IsDigitsOnly(UserInput) == false) || (UserInput == ""));
		
		int UserInputInt = Convert.ToInt32(UserInput);  // this is possible, because it is always only numbers (see 2 lines above)

		try{

			TicketLogic.ReserveFilteredTicket(filteredList[UserInputInt-1].Id);

		}catch(ArgumentOutOfRangeException){

			Console.WriteLine("This number is not on the list");
			ShowFilteredMovies(filteredList);

		}
	}


	public static void FilterMovies(){

		MenuUtils.displayFilters();

	}

}