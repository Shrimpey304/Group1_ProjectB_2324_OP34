using System.Data.Common;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
namespace Cinema;

public static class DisplayRoom{

    /// <summary>
    /// used to select seats in any cinema room
    /// </summary>
    /// <param name="fileName"></param>
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
                
                List<Seating> TempSeatingJson = SeatingJsonUtils.ReadFromJson(fileName);
                Seating tempSeating = TempSeatingJson[0];

                Console.Clear();

                if (tempSeating != null)
                {
                    for (int i = 0; i < tempSeating.Rows; i++)
                    {
                        for (int j = 0; j < tempSeating.Columns; j++)
                        {
                            if (tempSeating.SeatingArrangement[i,j] == tempSeating.SeatingArrangement[selectedPositionRow , selectedPositionCol]){

                                if(!tempSeating.SeatingArrangement[i,j][0].IsReserved){

                                    if (tempSeating.SeatingArrangement[i,j][0].Type == SeatType.Normal){
                                        Console.BackgroundColor = ConsoleColor.Green;
                                        Console.Write($"[ N ]");
                                    }else if(tempSeating.SeatingArrangement[i,j][0].Type == SeatType.Deluxe){
                                        Console.BackgroundColor = ConsoleColor.Green;
                                        Console.Write($"[ D ]");
                                    }else if(tempSeating.SeatingArrangement[i,j][0].Type == SeatType.Premium){
                                        Console.BackgroundColor = ConsoleColor.Green;
                                        Console.Write($"[ P ]");
                                    }

                                }else if(tempSeating.SeatingArrangement[i,j][0].inPrereservation){
                                    
                                    Console.BackgroundColor = ConsoleColor.Green;
                                    Console.Write($"[ S ]");

                                }else{

                                    Console.BackgroundColor = ConsoleColor.DarkGray;
                                    Console.Write($"[ R ]");

                                }

                            }else{

                                if(!tempSeating.SeatingArrangement[i,j][0].IsReserved){

                                    if(tempSeating.SeatingArrangement[i,j][0].inPrereservation){
                                        Console.BackgroundColor = ConsoleColor.DarkMagenta;
                                        Console.Write($"[ S ]");
                                    }
                                    else if(tempSeating.SeatingArrangement[i,j][0].Type == SeatType.Deluxe){
                                        Console.BackgroundColor = ConsoleColor.Blue;
                                        Console.Write($"[ D ]");
                                    }else if(tempSeating.SeatingArrangement[i,j][0].Type == SeatType.Premium){
                                        Console.BackgroundColor = ConsoleColor.Red;
                                        Console.Write($"[ P ]");
                                    }else if (tempSeating.SeatingArrangement[i,j][0].Type == SeatType.Normal){
                                        Console.BackgroundColor = ConsoleColor.Yellow;
                                        Console.Write($"[ N ]");
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

                    Console.Write($"Selected seats: Row: {SelectedPositions[0].Item1} Seat ");

                    foreach(Tuple<int,int> seatLoc in SelectedPositions){

                        Console.Write($"- {seatLoc.Item2}");
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

                            if(tempSeating!.SeatingArrangement[selectedPositionRow,selectedPositionCol][0].inPrereservation == false){

                                tempSeating.SeatingArrangement[selectedPositionRow,selectedPositionCol][0].inPrereservation = true;
                                SelectedPositions.Add(lastPos);

                            }else{

                                tempSeating.SeatingArrangement[selectedPositionRow,selectedPositionCol][0].inPrereservation = false;
                                SelectedPositions.Remove(lastPos);

                            }
                        break;
                        case ConsoleKey.Backspace:

                            List<Seating> UploadSeatingPreAdjustment = new(){seating};
                            SeatingJsonUtils.UploadToJson(UploadSeatingPreAdjustment, fileName);
                            
                        break;
                        
                    }
                }catch(Exception ex ){
                    Console.WriteLine(ex.Message);
                }

                List<Seating> TempUploadSeating = new(){tempSeating!};

                SeatingJsonUtils.UploadToJson(TempUploadSeating, fileName);
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
                    Type = SeatType.Normal,
                    inPrereservation = false

                });
            }
        }

        seatingInstance.Add(seating);
        SeatingJsonUtils.UploadToJson(seatingInstance, filePath);

    }

}
