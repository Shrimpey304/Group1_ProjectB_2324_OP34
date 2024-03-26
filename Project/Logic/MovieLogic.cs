// namespace Cinema;
// public class MovieLogic
// {
// 	public int AgeRestriction
// 	{
// 		get{
// 			return AgeRestriction;
// 		}
// 		set
// 		{
// 			if (value < MovieModel.MinimumAgeRestriction)
// 			{
// 				AgeRestriction = MinimumAgeRestriction;
// 			}
// 			else
// 			{
// 				AgeRestriction = value;
// 			}
// 		}
// 	}




// 	public static void AddMovie(Movie movie)
// 	{
// 		List<Movie> MovieList = MoviesJsonUtils.ReadFromJson("Movies.json");

// 		MovieList.Add(movie);

// 		MoviesJsonUtils.UploadToJson(MovieList, "Movies.Json");
// 	}

// 	public static void ListAllMovies()
// 	{
// 		List<Movie> MovieList = MoviesJsonUtils.ReadFromJson("Movies.json");
// 		foreach (Movie movie in MovieList)
// 		{
// 			Console.WriteLine(movie.Title, movie.GenreName, movie.AgeRestriction);
// 		}
// 	}
// }
