namespace Cinema;

public class SnackMenuModel
{
    public string SnackID { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string ExtraInfo { get; set; }


    public SnackMenuModel(string snackID, string name, decimal price, string description, string extraInfo)
    {
        SnackID = snackID;
        Name = name;
        Price = price;
        Description = description;
        ExtraInfo = extraInfo;
    }
}
