
namespace Cinema;

public class MovieModel
{

	public string Title;
	public static int mID = 1;
	public int movieID;
	public string Description;
	public string GenreName;
	public int AgeRestriction;
	
	
	public MovieModel(string title, int ageRest, string genreName, string description) 
	{
		Title = title;
		Description = description;
		AgeRestriction = ageRest;
		GenreName = genreName;
		movieID = mID ++;
	}
}


