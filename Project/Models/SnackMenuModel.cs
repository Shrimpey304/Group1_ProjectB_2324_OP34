namespace Cinema;

public class SnackMenuModel
{
    public int SnackID { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }

    public SnackMenuModel(int snackID, string name, decimal price, string description)
    {
        SnackID = snackID;
        Name = name;
        Price = price;
        Description = description;
    }
}
