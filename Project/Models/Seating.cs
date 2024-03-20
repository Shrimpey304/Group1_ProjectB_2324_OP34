namespace Cinema;

public class Seating
{
    public int Rows { get; set; }
    public int Columns { get; set; }
    public List<SeatInfo>[,] SeatingArrangement;

    public Seating(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;
        SeatingArrangement = new List<SeatInfo>[Rows,Columns];
    }
}