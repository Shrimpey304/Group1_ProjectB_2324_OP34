namespace Cinema;

public class Movie:Genre
{

    public string Title;

    public string Description;

    public Movie(string name, int ageRest, string Description) : base(name, ageRest)
    {
        
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