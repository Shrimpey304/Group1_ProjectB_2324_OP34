using Cinema;

public class AdminFuncUI{

	public static void adminChangeSeatTypes(){

		List<string> files = DisplayRoom.getFileDir();

		int cnt = 0;
		foreach(var file in files){
			Console.WriteLine($"roomnumber: {cnt+1}\nfile:{file}");
			Console.WriteLine("----------------------------");
			cnt++;
		}
		Console.Write("room to adjust >> ");
		string inp = Console.ReadLine()!;
		int intinp = Convert.ToInt32(inp);

		string selected = files[intinp-1];

		DisplayRoomUI.changeSeattype(selected);
	}


    public static void adminCreateRoom(){
		DisplayHeaderUI.AdminHeader();
		Console.WriteLine("\n---------------------------------------------------------------------------\n");
		Console.WriteLine("How many rows will your room have\n");
		Console.Write(">>> ");
		string rows = Console.ReadLine()!;
		int introws = Convert.ToInt32(rows);

		Console.Clear();
		DisplayHeaderUI.AdminHeader();
		Console.WriteLine("\n---------------------------------------------------------------------------\n");
		Console.WriteLine("How many Columns will your room have\n");
		Console.Write(">>> ");
		string cols = Console.ReadLine()!;
		int intcols = Convert.ToInt32(cols);

		DisplayRoom.CreateNewDefaultJson(introws, intcols);

		Console.WriteLine("room created succesfully");
	}


	public static void adminAddMovie(){
		try{
			Console.Clear();
			DisplayHeaderUI.AdminHeader();
			Console.WriteLine("\n---------------------------------------------------------------------------\n");			
			Console.WriteLine("What is the Movie's title?\n");
			Console.Write(">>> ");
			string movieName = Console.ReadLine()!;
			
			Console.Clear();
			DisplayHeaderUI.AdminHeader();
			Console.WriteLine("\n---------------------------------------------------------------------------\n");
			Console.WriteLine($"How old does a person have to be for {movieName}?\n");
			Console.Write(">>> ");
			string ageRestriction = Console.ReadLine()!;
			int intageRestriction = Convert.ToInt32(ageRestriction);

			Console.Clear();
			DisplayHeaderUI.AdminHeader();
			Console.WriteLine("\n---------------------------------------------------------------------------\n");
			Console.WriteLine($"What Genre is {movieName}?\n");
			Console.Write(">>> ");
			string genre = Console.ReadLine()!;

			Console.Clear();
			DisplayHeaderUI.AdminHeader();
			Console.WriteLine("\n---------------------------------------------------------------------------\n");
			Console.WriteLine($"Enter a description for: {movieName}\n");
			Console.Write(">>> ");
			string description = Console.ReadLine()!;

			MovieModel movie = new MovieModel(movieName, intageRestriction, genre, description);
			MovieLogic.AddMovie(movie);

			Console.Clear();
			DisplayHeaderUI.AdminHeader();
			Console.WriteLine("\n---------------------------------------------------------------------------\n");
			Console.WriteLine($"Movie Added\n");
		}catch{
			Console.WriteLine("invalid input");
			MenuUtils.displayLoggedinAdminMenu();
		}

		return;
	}

	public static void adminAddSession(){

		try{
			Console.Clear();
			DisplayHeaderUI.AdminHeader();
			Console.WriteLine("\n---------------------------------------------------------------------------\n");			
			int selectedID = MovieUI.ListAllMovies();

			Console.Clear();
			DisplayHeaderUI.AdminHeader();
			Console.WriteLine("\n---------------------------------------------------------------------------\n");
			Console.WriteLine($"what date do you want to schedule this session (dd-mm-yyyy)\n");
			Console.Write(">>> ");
			string dateInput = Console.ReadLine()!;
	

			Console.Clear();
			DisplayHeaderUI.AdminHeader();
			Console.WriteLine("\n---------------------------------------------------------------------------\n");
			Console.WriteLine($"what time do you want to schedule this session (hh:mm:ss)\n");
			Console.Write(">>> ");
			string timeInput = Console.ReadLine()!;

			Console.Clear();
			DisplayHeaderUI.AdminHeader();
			Console.WriteLine("\n---------------------------------------------------------------------------\n");
			Console.WriteLine($"what is the duration of this movie (hh:mm:ss)\n");
			Console.Write(">>> ");
			string durationInput = Console.ReadLine()!;

			Console.Clear();
			DisplayHeaderUI.AdminHeader();
			Console.WriteLine("\n---------------------------------------------------------------------------\n");
			Console.WriteLine($"what room number will the movie play in\n");
			Console.Write(">>> ");
			string roomnumber = Console.ReadLine()!;
			int introomnumber = Convert.ToInt32(roomnumber);

			// Parse date and time inputs
			DateTime startTime = DateTime.ParseExact(dateInput + " " + timeInput, "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
			TimeSpan duration = TimeSpan.Parse(durationInput);

			// Calculate end time
			DateTime endTime = startTime.Add(duration);

			MovieSessionModel session = new MovieSessionModel(startTime, endTime, selectedID, introomnumber);

			MovieLogic.AddMovieSession(session);
		
		}catch (Exception ex)
		{
			Console.WriteLine($"Error: {ex.Message}");
			Console.WriteLine("Invalid input");
			return;
		}

		Console.Clear();
		DisplayHeaderUI.AdminHeader();
		Console.WriteLine("\n---------------------------------------------------------------------------\n");
		Console.WriteLine($"Session Added\n");

		return;
	}

}