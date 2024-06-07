namespace Cinema;

public class SnackMenuModel : IID
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string ExtraInfo { get; set; }


    public SnackMenuModel(int id, string name, decimal price, string description, string extraInfo)
    {
        Id = id;
        Name = name;
        Price = price;
        Description = description;
        ExtraInfo = extraInfo;
    }
}
