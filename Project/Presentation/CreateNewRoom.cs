namespace Cinema;

public class CreateNewRoom{

    public static void MakeNew(){
        int rowInt; 
        int colInt;

        Console.WriteLine("how many rows do you want to have? (vertical amount of seats)");
        string ?rowsString = Console.ReadLine();
        if (rowsString is not null){
            rowInt = Convert.ToInt32(rowsString);
        }else{
            rowInt = 10;
        }
        Console.WriteLine("how many columns do you want to have? (horizontal amount of seats)");
        string ?colsString = Console.ReadLine();
        if (rowsString is not null){
            colInt = Convert.ToInt32(rowsString);
        }else{
            colInt = 10;
        }
        Console.WriteLine();
        DisplayRoom.CreateNewDefaultJson(rowInt, colInt);
    }

}