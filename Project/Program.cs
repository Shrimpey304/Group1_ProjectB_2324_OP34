namespace Cinema;

public class Program{

    public static void Main(){

        Seating seating = new Seating(10, 10);
        DisplayRoom.DisplaySeatingIDs(seating.SeatingArrangement, seating.Rows, seating.Columns);

    }

}