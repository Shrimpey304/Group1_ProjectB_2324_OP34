using System;
using System.Collections.Generic;
using System.Globalization;

namespace Cinema;
public class AdminFuncUI
{
	public static void adminChangeSeatTypes()
	{
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

	public static void adminAddMovie()
	{
		try
		{
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
		}
		catch
		{
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
		string IdInput = Console.ReadLine()!;

		if (string.IsNullOrWhiteSpace(IdInput))
		{
			Console.WriteLine("Update canceled. No movie ID provided.");
			return;
		}

		if (!int.TryParse(IdInput, out int Id))
		{
			Console.WriteLine("Invalid movie ID format. Please enter a valid integer ID.");
			return;
		}

		// Fetch the movie with the specified ID
		MovieModel movieToEdit = MovieLogic.GetMovieByID(Id);

		if (movieToEdit != null)
		{
			Console.WriteLine($"Current details of movie with ID {movieToEdit.Id}:");
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
			Console.WriteLine($"Movie with ID {movieToEdit.Id} has been updated.");
		}
		else
		{
			Console.WriteLine($"No movie found with ID {Id}.");
		}
	}

	public static void AdminDeleteMovie()
	{
		DisplayHeaderUI.AdminHeader();
		Console.WriteLine("\n---------------------------------------------------------------------------\n");
		MovieUI.ShowMovies();
		Console.WriteLine("Enter the ID of the movie you want to delete:");
		string IdInput = Console.ReadLine()!;

		if (string.IsNullOrWhiteSpace(IdInput))
		{
			Console.WriteLine("Deletion canceled. No movie ID provided.");
			return;
		}

		if (!int.TryParse(IdInput, out int Id))
		{
			Console.WriteLine("Invalid movie ID format. Please enter a valid integer ID.");
			return;
		}

		// Delete the movie with the specified ID
		bool success = MovieLogic.DeleteMovie(Id);

		if (success)
		{
			Console.WriteLine($"Movie with ID {Id} has been deleted.");
		}
		else
		{
			Console.WriteLine($"No movie found with ID {Id}.");
		}
	}

	public static void adminAddSession()
	{
		try
		{
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
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error: {ex.Message}");
			Console.WriteLine("Invalid input");
			return;
		}

		Console.Clear();
		DisplayHeaderUI.AdminHeader();
		Console.WriteLine("\n---------------------------------------------------------------------------\n");
		Console.WriteLine($"Session Added\n");
	}
	public static void adminUpdateSession()
	{
		Console.Clear();
		DisplayHeaderUI.AdminHeader();
		MovieUI.ShowSessions();
		Console.WriteLine("\n---------------------------------------------------------------------------\n");
		Console.WriteLine("Enter the ID of the session you want to update:");
		int sessionID;
		if (!int.TryParse(Console.ReadLine(), out sessionID))
		{
			Console.WriteLine("Invalid session ID.");
			return;
		}

		// Fetch the session with the specified ID
		MovieSessionModel sessionToUpdate = MovieLogic.GetSession(sessionID);

		if (sessionToUpdate != null)
		{
			Console.WriteLine($"Current details of session with ID {sessionToUpdate.sessionID}:");

			// Prompt the admin to enter new details for the session
			Console.WriteLine("Enter the new date for the session (dd-MM-yyyy):");
			string dateInput = Console.ReadLine()!;
			DateTime newStartTime;
			if (!DateTime.TryParseExact(dateInput, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out newStartTime))
			{
				Console.WriteLine("Invalid date format.");
				return;
			}

			Console.WriteLine("Enter the new starting time for the session (HH:mm:ss):");
			string startTimeInput = Console.ReadLine()!;
			DateTime startTime;
			if (!DateTime.TryParseExact(startTimeInput, "HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out startTime))
			{
				Console.WriteLine("Invalid start time format.");
				return;
			}

			Console.WriteLine("Enter the new ending time for the session (HH:mm:ss):");
			string endTimeInput = Console.ReadLine()!;
			DateTime endTime;
			if (!DateTime.TryParseExact(endTimeInput, "HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out endTime))
			{
				Console.WriteLine("Invalid end time format.");
				return;
			}

			Console.WriteLine("Enter the new movie ID:");
			string IdInput = Console.ReadLine()!;
			int newId;
			if (!int.TryParse(IdInput, out newId))
			{
				Console.WriteLine("Invalid movie ID format.");
				return;
			}

			Console.WriteLine("Enter the new room ID:");
			string roomIDInput = Console.ReadLine()!;
			int newRoomID;
			if (!int.TryParse(roomIDInput, out newRoomID))
			{
				Console.WriteLine("Invalid room ID format.");
				return;
			}

			// Modify session details as required
			sessionToUpdate.StartTime = new DateTime(newStartTime.Year, newStartTime.Month, newStartTime.Day, startTime.Hour, startTime.Minute, startTime.Second);
			sessionToUpdate.EndTime = new DateTime(newStartTime.Year, newStartTime.Month, newStartTime.Day, endTime.Hour, endTime.Minute, endTime.Second);
			sessionToUpdate.Id = newId;
			sessionToUpdate.RoomID = newRoomID;

			// Update the session
			MovieLogic.UpdateMovieSession(sessionID, sessionToUpdate);
			Console.WriteLine($"Session with ID {sessionToUpdate.sessionID} has been updated.");
		}
		else
		{
			Console.WriteLine($"No session found with ID {sessionID}.");
		}
	}

	public static void adminDeleteSession()
	{
		Console.Clear();
		DisplayHeaderUI.AdminHeader();
		MovieUI.ShowSessions();
		Console.WriteLine("\n---------------------------------------------------------------------------\n");
		Console.WriteLine("Enter the ID of the session you want to delete:");
		string sessionIDInput = Console.ReadLine()!;

		// Check if input is empty
		if (string.IsNullOrWhiteSpace(sessionIDInput))
		{
			Console.WriteLine("Deletion canceled. No session ID provided.");
			return;
		}

		// Parse session ID
		if (!int.TryParse(sessionIDInput, out int sessionID))
		{
			Console.WriteLine("Invalid session ID format. Please enter a valid integer ID.");
			return;
		}

		// Delete the session with the specified ID
		bool success = MovieLogic.DeleteMovieSession(sessionID);

		if (success)
		{
			Console.WriteLine($"Session with ID {sessionID} has been deleted.");
		}
		else
		{
			Console.WriteLine($"No session found with ID {sessionID}.");
		}
	}
}

