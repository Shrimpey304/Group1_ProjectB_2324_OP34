using System.Data.Common;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
namespace Cinema;

public static class DisplayRoom{

    /// <summary>
    /// temporary test to tell if what im doing is correct
    /// </summary>
    /// <param name="seating"></param>
    public static void DisplaySeatingIDs(string fileName, bool wantSeatTypeInstead)
    {
        try
        {   
            List<Seating> seatingJson = SeatingJsonUtils.ReadFromJson(fileName);
            Seating seating = seatingJson[0];

            if (seating != null)
            {
                Console.WriteLine("Seating IDs Grid:");

                for (int i = 0; i < seating.Rows; i++)
                {
                    for (int j = 0; j < seating.Columns; j++)
                    {
                        if (!wantSeatTypeInstead){
                            Console.Write($"[{seating.SeatingArrangement[i, j][0].RowID},{seating.SeatingArrangement[i, j][0].ColumnID}]");
                        }else{

                            if(!seating.SeatingArrangement[i,j][0].IsReserved){

                                if (seating.SeatingArrangement[i,j][0].Type == SeatType.Normal){
                                    Console.BackgroundColor = ConsoleColor.Yellow;
                                    Console.Write($"[ N ]");
                                }else if(seating.SeatingArrangement[i,j][0].Type == SeatType.Deluxe){
                                    Console.BackgroundColor = ConsoleColor.Blue;
                                    Console.Write($"[ D ]");
                                }else if(seating.SeatingArrangement[i,j][0].Type == SeatType.Premium){
                                    Console.BackgroundColor = ConsoleColor.Red;
                                    Console.Write($"[ P ]");
                                }
                            }else{
                                Console.BackgroundColor = ConsoleColor.DarkGray;
                                Console.Write($"[ R ]");
                            }
                        }
                    }
                    Console.WriteLine();
                }
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("Failed to load seating arrangement from JSON.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error displaying seating: {ex.Message}");
        }
    }

    /// <summary>
    /// used to select seats in any cinema room
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="wantSeatTypeInstead"></param>
    public static void SelectSeating(string fileName){
        try
        {   
            List<Seating> seatingJson = SeatingJsonUtils.ReadFromJson(fileName);
            Seating seating = seatingJson[0];

            int selectedPositionCol = 0;
            int selectedPositionRow = 0;

            //item 1 = row || item 2 = col
            List<Tuple<int,int>> SelectedPositions = new();    
        
            while(!Console.KeyAvailable){
                
                Console.Clear();

                if (seating != null)
                {
                    for (int i = 0; i < seating.Rows; i++)
                    {
                        for (int j = 0; j < seating.Columns; j++)
                        {
                            if (seating.SeatingArrangement[i,j] == seating.SeatingArrangement[selectedPositionRow , selectedPositionCol]){

                                if(!seating.SeatingArrangement[i,j][0].IsReserved){

                                    if (seating.SeatingArrangement[i,j][0].Type == SeatType.Normal){
                                        Console.BackgroundColor = ConsoleColor.Green;
                                        Console.Write($"[ N ]");
                                    }else if(seating.SeatingArrangement[i,j][0].Type == SeatType.Deluxe){
                                        Console.BackgroundColor = ConsoleColor.Green;
                                        Console.Write($"[ D ]");
                                    }else if(seating.SeatingArrangement[i,j][0].Type == SeatType.Premium){
                                        Console.BackgroundColor = ConsoleColor.Green;
                                        Console.Write($"[ P ]");
                                    }
                                }else{
                                    Console.BackgroundColor = ConsoleColor.DarkGray;
                                    Console.Write($"[ R ]");
                                }

                            }else{

                                if(!seating.SeatingArrangement[i,j][0].IsReserved){

                                    if (seating.SeatingArrangement[i,j][0].Type == SeatType.Normal){
                                        Console.BackgroundColor = ConsoleColor.Yellow;
                                        Console.Write($"[ N ]");
                                    }else if(seating.SeatingArrangement[i,j][0].Type == SeatType.Deluxe){
                                        Console.BackgroundColor = ConsoleColor.Blue;
                                        Console.Write($"[ D ]");
                                    }else if(seating.SeatingArrangement[i,j][0].Type == SeatType.Premium){
                                        Console.BackgroundColor = ConsoleColor.Red;
                                        Console.Write($"[ P ]");
                                    }
                                }else{
                                    Console.BackgroundColor = ConsoleColor.DarkGray;
                                    Console.Write($"[ R ]");
                                }
                            }
                        }
                        Console.WriteLine();
                    }
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("Failed to load seating arrangement from JSON.");
                }
                
                Tuple<int,int> lastPos = new Tuple<int, int>(selectedPositionRow,selectedPositionCol);

                if(SelectedPositions.Count > 0){

                    Console.Write($"Selected seats: Row: {SelectedPositions[0].Item1} Seat: ");

                    foreach(Tuple<int,int> seatLoc in SelectedPositions){

                        Console.Write($" {seatLoc.Item2} -");
                    }

                }else{

                    Console.WriteLine("Selected seats: None");
                }

                try{
                    switch(Console.ReadKey(true).Key){
                        case ConsoleKey.UpArrow:
                            if((selectedPositionRow - 1) < 0){
                                selectedPositionRow = lastPos.Item1;
                            }else{
                                selectedPositionRow --;
                            }
                        break;
                        case ConsoleKey.DownArrow:
                            if((selectedPositionRow +1) > (seating.Rows -1)){
                                selectedPositionRow = lastPos.Item1;
                            }else{
                                selectedPositionRow ++;
                            }
                        break;
                        case ConsoleKey.LeftArrow:
                            if((selectedPositionCol - 1) < 0){
                                selectedPositionCol = lastPos.Item2;
                            }else{
                                selectedPositionCol --;
                            }
                        break;
                        case ConsoleKey.RightArrow:
                            if((selectedPositionCol +1) > (seating.Columns -1)){
                                selectedPositionCol = lastPos.Item2;
                            }else{
                                selectedPositionCol ++;
                            }
                        break;
                        case ConsoleKey.Enter:
                            Console.WriteLine($"selected:{selectedPositionRow},{selectedPositionCol}");
                            SelectedPositions.Add(lastPos);
                        break;
                    }
                }catch(Exception ex ){
                    Console.WriteLine(ex.Message);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error displaying seating: {ex.Message}");
        }
    }

    /// <summary>
    /// used for creating new cinema rooms
    /// </summary>
    /// <param name="rows"></param>
    /// <remarks> to create a new room json file: string "DataStorage/yourjsonname.json" </remarks>
    /// <param name="cols"></param>
    /// <param name="filePath"></param>
    public static void CreateNewDefaultJson(int rows, int cols, string filePath)
    {
        List<Seating> seatingInstance = new();

        int Rows = rows;
        int Columns = cols;

        Seating seating = new Seating(Rows, Columns);

        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                if (seating.SeatingArrangement[i, j] == null)
                {
                    seating.SeatingArrangement[i, j] = new List<SeatInfo>();
                }
                seating.SeatingArrangement[i, j].Add(new SeatInfo{

                    RowID = i,
                    ColumnID = j,
                    Price = 10.0,
                    IsReserved = false,
                    Type = SeatType.Normal

                });
            }
        }

        seatingInstance.Add(seating);
        SeatingJsonUtils.UploadToJson(seatingInstance, filePath);

    }

}
