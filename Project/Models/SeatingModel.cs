namespace Cinema;

public class Seating
{
    public int ID { get; set; }
    public int Rows { get; set; }
    public int Columns { get; set; }
    public List<SeatInfo>[,] SeatingArrangement;

    public Seating(int rows, int columns, int id)
    {
        ID = id;
        Rows = rows;
        Columns = columns;
        SeatingArrangement = new List<SeatInfo>[Rows,Columns];
    }
}