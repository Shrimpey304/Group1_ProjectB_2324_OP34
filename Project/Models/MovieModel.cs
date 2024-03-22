using System.ComponentModel.DataAnnotations;

namespace Cinema;

public class Movie:Genre
{
//ID toevoegen
	public string Title;

	public static int mID = 1;
	public int movieID;
	public string Description;
	
	public Movie(string title, int ageRest, string genreName, string description) : base(genreName, ageRest)
	{
		Title = title;
		Description = description;
		movieID = mID ++;
	}


	public int AgeRestriction
	{
		get{
			return AgeRestriction;
		}
		set
		{
			if (value < MinimumAgeRestriction)
			{
				AgeRestriction = MinimumAgeRestriction;
			}
			else
			{
				AgeRestriction = value;
			}
		}
	}




	public static void AddMovie(Movie movie)
	{
		List<Movie> MovieList = MoviesJsonUtils.ReadFromJson("Movies.json");

		MovieList.Add(movie);

		MoviesJsonUtils.UploadToJson(MovieList, "Movies.Json");
	}

	public static void ListAllMovies()
	{
		List<Movie> MovieList = MoviesJsonUtils.ReadFromJson("Movies.json");
		foreach (Movie movie in MovieList)
		{
			Console.WriteLine(movie.Title, movie.GenreName, movie.AgeRestriction);
		}
	}
}