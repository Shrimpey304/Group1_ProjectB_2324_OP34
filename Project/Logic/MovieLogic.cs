namespace Cinema;
public class MovieLogic
{
	const string filePathMovies = "DataStorage/Movies.json";
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
		List<MovieModel> MovieList = JsonAccess.ReadFromJson<MovieModel>(filePathMovies);

		MovieList.Add(movie);

		JsonAccess.UploadToJson(MovieList, "Movies.Json");
	}

}