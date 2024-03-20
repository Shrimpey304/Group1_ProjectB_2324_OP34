using System.ComponentModel.DataAnnotations;

namespace Cinema;

public class Movie:Genre
{
//ID toevoegen
	public string Title;

	public string Description;

	public Movie(string title, int ageRest, string genreName, string description) : base(genreName, ageRest)
	{
		Title = title;
		Description = description;
	}

	public void AddMovie(Movie movie)
	{
		List<Movie> MovieList = MoviesJsonUtils.ReadFromJson("Movies.json");

		MovieList.Add(movie);
		
		MoviesJsonUtils.UploadToJson(MovieList, "Movies.Json");
	}

	

	public int AgeRestriction
	{
		get{return AgeRestriction;}
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


}