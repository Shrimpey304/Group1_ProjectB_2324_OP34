namespace Cinema;

public class Program{

	public static void Main(){
	  

		const string fileName = "DataStorage/CinemaRoom1.json";
		const string fileNameSesh = "DataStorage/Sessions.json";
		// MovieSession.MakeSession(DateTime.Now, DateTime.Now.AddHours(1));
		
		
		// List<MovieSession> sessions = SessionsJsonUtils.ReadFromJson(fileNameSesh);
		// MovieSession sesh = sessions[0];

		// DisplayRoom.SelectSeating(fileName, sesh);
		Menu.Start(); // Start the application menu
		
		// Movie m1 = new Movie("title 1", 13, "Not Horror", "Very not-scary");
		// Movie m2 = new Movie("Scary radio", 18, "Very scared", "OH NO");
		// Movie.AddMovie(m1);
		// Movie.AddMovie(m2);
		
		// Movie.ListAllMovies();
	}
}