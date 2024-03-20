namespace Cinema;

public class Movie:Genre
{

	public string Title;

	public string Description;

	public Movie(string title, int ageRest, string genreName, string description) : base(genreName, ageRest)
	{
		Title = title;
		Description = description;
	}

	List<Movie> MovieList = new List<Movie>();

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