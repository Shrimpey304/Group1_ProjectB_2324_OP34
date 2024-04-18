namespace Cinema;

public class MovieModel
{

	public string Title;
	public static int mID = 1;
	public int MovieID;
	public string Description;
	public string GenreName;
	public int AgeRestriction;
	public int RoomID;
	
	
	
	public MovieModel(string title, int ageRest, string genreName, string description, int rID) 
	{
		Title = title;
		Description = description;
		AgeRestriction = ageRest;
		GenreName = genreName;
		MovieID = mID ++;
		RoomID = rID;
	}
}


