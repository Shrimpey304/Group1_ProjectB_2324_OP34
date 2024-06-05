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


    public static void adminCreateRoom()
	{
		DisplayHeaderUI.AdminHeader();
		Console.WriteLine("\n---------------------------------------------------------------------------\n");

		int introws = DisplayRoom.GetValidSize("How many rows will your room have (0-35)? ");
		int intcols = DisplayRoom.GetValidSize("How many columns will your room have (0-35)? ");

		DisplayRoom.CreateNewDefaultJson(introws, intcols);

		Console.WriteLine("Room created successfully");
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

	

	public static void AdminEditMovie()
	{   
		DisplayHeaderUI.AdminHeader();
		Console.WriteLine("\n---------------------------------------------------------------------------\n"); 
		MovieUI.ShowMovies();           
		Console.WriteLine("Enter the ID of the movie you want to edit:");
		int movieId = int.Parse(Console.ReadLine()!);

		// Fetch the movie with the specified ID
		MovieModel movieToEdit = MovieLogic.GetMovieByID(movieId);

		if (movieToEdit != null)
		{
			Console.WriteLine($"Current details of movie with ID {movieToEdit.MovieID}:");
			MovieLogic.DisplayMovieDetails(movieToEdit);

			// Prompt the admin to enter new details for the movie
			Console.WriteLine("Enter new title (leave empty to keep current):");
			string newTitle = Console.ReadLine()!;
			if (!string.IsNullOrEmpty(newTitle))
			{
				movieToEdit.Title = newTitle;
			}

			Console.WriteLine("Enter new age restriction (leave empty to keep current):");
			string newAgeRestriction = Console.ReadLine()!;
			if (!string.IsNullOrEmpty(newAgeRestriction))
			{
				movieToEdit.AgeRestriction = int.Parse(newAgeRestriction);
			}

			Console.WriteLine("Enter new description (leave empty to keep current):");
			string newDescription = Console.ReadLine()!;
			if (!string.IsNullOrEmpty(newDescription))
			{
				movieToEdit.Description = newDescription;
			}

			Console.WriteLine("Enter new genre (leave empty to keep current):");
			string newGenre = Console.ReadLine()!;
			if (!string.IsNullOrEmpty(newGenre))
			{
				movieToEdit.GenreName = newGenre;
			}

			// Update the movie in the data storage
			MovieLogic.UpdateMovie(movieToEdit);
			Console.WriteLine($"Movie with ID {movieToEdit.MovieID} has been updated.");
		}
		else
		{
			Console.WriteLine($"No movie found with ID {movieId}.");
		}
	}

	public static void AdminDeleteMovie()
	{
		DisplayHeaderUI.AdminHeader();
		Console.WriteLine("\n---------------------------------------------------------------------------\n");            
		MovieUI.ShowMovies();
		Console.WriteLine("Enter the ID of the movie you want to delete:");
		int movieId = int.Parse(Console.ReadLine()!);

		// Delete the movie with the specified ID
		bool success = MovieLogic.DeleteMovie(movieId);

		if (success)
		{
			Console.WriteLine($"Movie with ID {movieId} has been deleted.");
		}
		else
		{
			Console.WriteLine($"No movie found with ID {movieId}.");
		}
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