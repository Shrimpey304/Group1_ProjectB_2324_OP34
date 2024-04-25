using System.Data.Common;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
namespace Cinema;

public static class DisplayRoom{
  
    /// <summary>
    /// used to select seats in any cinema room
    /// </summary>
    /// <param name="fileName"></param>
    public static List<Tuple<int,int>> SelectSeating(MovieSessionModel session){
        try
        {  
            string fileNM = "";
            string DataStoragePath = @"DataStorage/"; 
            string[] filelist = Directory.GetFiles(DataStoragePath);


            if (filelist != null){

                foreach(string file in filelist){
                    try{
                        if (file.ToLower() == $"datastorage/cinemaroom{Convert.ToString(session.RoomID)}.json"){
                            // List<Seating> sitdown = JsonAccess.ReadFromJson<Seating>(file);
                            List<Seating>fileJson = JsonAccess.ReadFromJson<Seating>($"{file}");
                            if (fileJson != null && fileJson.Count > 0){
                                fileNM = file;
                            }else{
                                continue;
                            }
                        }
                    }catch(Exception e){
                        Console.WriteLine(e);
                        continue;
                    }
                }
            }

            Console.WriteLine($"{fileNM}");
            List<Seating> seatingJson = JsonAccess.ReadFromJson<Seating>(fileNM);
            Seating seating = seatingJson[0];

            int selectedPositionCol = 0;
            int selectedPositionRow = 0;

            //item 1 = row    item 2 = col
            List<Tuple<int,int>> SelectedPositions = new();    
        
            while(!Console.KeyAvailable){

                List<Seating> TempSeatingJson = JsonAccess.ReadFromJson<Seating>(fileNM); //will be used for a function later
                Seating tempSeating = TempSeatingJson[0];

                Console.Clear();

                if (tempSeating != null)
                {
                    Console.Write("".PadLeft(3));
                    for (int col = 0; col < tempSeating.Columns; col++){
                        Console.ResetColor();

                        if(col >= 10){

                            Console.Write($" {col}  ");

                        }else if(col >= 100){

                            Console.Write($" {col} ");

                        }else{

                            Console.Write($"  {col}  ");

                        }
                    }
                    Console.WriteLine("");
                    for (int i = 0; i < tempSeating.Rows; i++)
                    {
                        Console.ResetColor();
                        Console.Write($"{i}  ".PadRight(3));
                        for (int j = 0; j < tempSeating.Columns; j++)
                        {
                            if (tempSeating.SeatingArrangement[i,j] == tempSeating.SeatingArrangement[selectedPositionRow , selectedPositionCol]){

                                if(tempSeating.SeatingArrangement[i,j][0].reservedInSession.Count() > 0){

                                    foreach(MovieSessionModel sesh in tempSeating.SeatingArrangement[i,j][0].reservedInSession){

                                        if(sesh.sessionID != session.sessionID){

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

                                        }else if(sesh.sessionID == session.sessionID){

                                            Console.BackgroundColor = ConsoleColor.DarkGray;
                                            Console.Write($"[ R ]");

                                        }
                                    }
                                }else{

                                    if(tempSeating.SeatingArrangement[i,j][0].inPrereservation){    
                                        Console.BackgroundColor = ConsoleColor.Green;
                                        Console.Write($"[ S ]");
                                    }else if (tempSeating.SeatingArrangement[i,j][0].Type == SeatType.Normal){
                                        Console.BackgroundColor = ConsoleColor.Green;
                                        Console.Write($"[ N ]");
                                    }else if(tempSeating.SeatingArrangement[i,j][0].Type == SeatType.Deluxe){
                                        Console.BackgroundColor = ConsoleColor.Green;
                                        Console.Write($"[ D ]");
                                    }else if(tempSeating.SeatingArrangement[i,j][0].Type == SeatType.Premium){
                                        Console.BackgroundColor = ConsoleColor.Green;
                                        Console.Write($"[ P ]");
                                    }
                                }

                            }else{

                                if(tempSeating.SeatingArrangement[i,j][0].reservedInSession.Count() > 0){

                                    foreach(MovieSessionModel sesh in tempSeating.SeatingArrangement[i,j][0].reservedInSession){

                                        if(sesh.sessionID != session.sessionID){

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
                                        }else if(sesh.sessionID == session.sessionID){

                                            Console.BackgroundColor = ConsoleColor.DarkGray;
                                            Console.Write($"[ R ]");

                                        }
                                    }
                                }else{

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
                                }

                            }
                        }
                        Console.WriteLine("");
                    }
                    Console.ResetColor();
                    Console.WriteLine("\n");
                    Console.Write("".PadLeft(3));
                    for (int col = 0; col < tempSeating.Columns; col++){
                        Console.ResetColor();
                        Console.Write("_____");
                    } //legenda
                    Console.BackgroundColor = ConsoleColor.Yellow; Console.Write("\n\n[N]".PadLeft(3)); Console.ResetColor(); Console.Write(" = Normal seat  ");
                    Console.BackgroundColor = ConsoleColor.Blue; Console.Write("[D]"); Console.ResetColor(); Console.Write(" = Deluxe seat  ");
                    Console.BackgroundColor = ConsoleColor.Red; Console.Write("[P]"); Console.ResetColor(); Console.Write(" = Premium seat  \n");
                    Console.BackgroundColor = ConsoleColor.DarkGray; Console.Write("[R]"); Console.ResetColor(); Console.Write(" = Reserved seat  ");
                    Console.BackgroundColor = ConsoleColor.Magenta; Console.Write("[S]"); Console.ResetColor(); Console.Write(" = Selected seat  ");
                    Console.Write("_____"); Console.ResetColor(); Console.Write(" = Screen  \n");
                    Console.Write("R"); Console.ResetColor(); Console.Write(" = Reserve  \n");
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
                            if (tempSeating!.SeatingArrangement[selectedPositionRow, selectedPositionCol][0].inPrereservation == false)
                            {
                                if (SelectedPositions.Count == 0 || SelectedSeatsInRow(SelectedPositions, selectedPositionRow, selectedPositionCol, tempSeating))
                                {
                                    tempSeating.SeatingArrangement[selectedPositionRow, selectedPositionCol][0].inPrereservation = true;
                                    SelectedPositions.Add(new Tuple<int, int>(selectedPositionRow, selectedPositionCol));
                                }
                                else
                                {
                                    Console.WriteLine("You can only select multiple connecting seats in the same row.");
                                }
                            }
                            else
                            {
                                if (SelectedPositions.Count >= 2)
                                {
                                    var sortedPositions = SelectedPositions.OrderBy(pos => pos.Item2).ToList();

                                    int index = sortedPositions.FindIndex(pos => pos.Item2 == selectedPositionCol);

                                    if (index > 0 && index < sortedPositions.Count - 1)
                                    {
                                        Console.WriteLine("This seat is between two selected seats and cannot be deselected.");
                                    }
                                    else
                                    {
                                        tempSeating.SeatingArrangement[selectedPositionRow, selectedPositionCol][0].inPrereservation = false;
                                        SelectedPositions.Remove(new Tuple<int, int>(selectedPositionRow, selectedPositionCol));
                                    }
                                }
                                else
                                {
                                    tempSeating.SeatingArrangement[selectedPositionRow, selectedPositionCol][0].inPrereservation = false;
                                    SelectedPositions.Remove(new Tuple<int, int>(selectedPositionRow, selectedPositionCol));
                                }
                            }
                        break;
                        case ConsoleKey.Backspace:

                            List<Seating> UploadSeatingPreAdjustment = new(){seating};
                            JsonAccess.UploadToJson(UploadSeatingPreAdjustment, fileNM);
                            
                        break;
                        case ConsoleKey.R:
                            
                            if(SelectedPositions is not null && SelectedPositions.Count != 0){
                                foreach(Tuple<int,int> pos in SelectedPositions){
                                    tempSeating.SeatingArrangement[pos.Item1, pos.Item2][0].reservedInSession.Add(session);
                                    tempSeating.SeatingArrangement[pos.Item1, pos.Item2][0].inPrereservation = false;
                                    List<Seating> uploadTempSeatingReserved = new(){tempSeating!};

                                    JsonAccess.UploadToJson(uploadTempSeatingReserved, fileNM);
                                }
                                Console.WriteLine("generating ticket for selected seats");
                                Thread.Sleep(2000);
                            }else{
                                Console.WriteLine("you need to select atleast 1 seat");
                                break;
                            }
                        return SelectedPositions;
                    }
                }catch(Exception ex ){
                    Console.WriteLine(ex.Message);
                    return null;
                }

                List<Seating> TempUploadSeating = new(){tempSeating!};

                JsonAccess.UploadToJson(TempUploadSeating, fileNM);
            }
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error displaying seating: {ex.Message} \n {ex.Data} \n {ex.GetBaseException()} \n {ex.GetObjectData} \n {ex.StackTrace}");
            return null;
        }
    }


    private static bool SelectedSeatsInRow(List<Tuple<int, int>> selectedPositions, int selectedPositionRow, int selectedPositionCol, Seating tempseating)
    {
        bool positionside1taken = false;
        bool positionside2taken = false;
        foreach (var pos in selectedPositions)
        {
            foreach(var positionsNext in selectedPositions){
                if((pos.Item2 + 1) == positionsNext.Item2){
                    positionside1taken = true;
                }else if((pos.Item2 - 1) == positionsNext.Item2){
                    positionside2taken = true;
                }
            }
            if(tempseating.SeatingArrangement[pos.Item1,pos.Item2][0].inPrereservation){
                if (pos.Item1 != selectedPositionRow || Math.Abs(pos.Item2 - selectedPositionCol) >= 8)
                {
                    if(positionside1taken && positionside2taken){
                        return false;
                    }
                    return false;
                }
            }else{
                tempseating.SeatingArrangement[pos.Item1,pos.Item2][0].inPrereservation = true;
                if (pos.Item1 != selectedPositionRow || Math.Abs(pos.Item2 - selectedPositionCol) >= 8)
                {
                    if(positionside1taken && positionside2taken){
                        return false;
                    }
                    return false;
                }
            }
        }
        return true;
    }


    /// <summary>
    /// used for creating new cinema rooms
    /// </summary>
    /// <param name="rows"></param>
    /// <remarks> to create a new room json file: string "DataStorage/yourjsonname.json" </remarks>
    /// <param name="cols"></param>
    /// <param name="filePath"></param>
    public static void CreateNewDefaultJson(int rows, int cols)
    {
        string DataStoragePath = @"DataStorage/";

        List<Seating> seatingInstance = new();

        int Rows = rows;
        int Columns = cols;

        string[] filelist = Directory.GetFiles(DataStoragePath);
        List<int> cinemaroomlist = new();

        if (filelist != null){

            foreach(string file in filelist){
                if (file.ToLower().Contains("cinemaroom")){
                    string[] splitfile = file.ToLower().Split("cinemaroom");
                    string[] splitfileID = splitfile[1].Split(".json");
                    int fileID = Convert.ToInt32(splitfileID[0]);
                    cinemaroomlist.Add(fileID);
                }
            }
        }

        if (cinemaroomlist is not null){

            int id = cinemaroomlist.Max() + 1;
            Seating seating = new Seating(Rows, Columns, id);
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
                        reservedInSession = new(),
                        Type = SeatType.Normal,
                        inPrereservation = false

                    });
                }
            }
            string newPath = $"{DataStoragePath}/CinemaRoom{id}.json";
            seatingInstance.Add(seating);
            JsonAccess.UploadToJson(seatingInstance, newPath);

        }else{

            int id = 1;
            Seating seating = new Seating(Rows, Columns, id);
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
                        reservedInSession = new(),
                        Type = SeatType.Normal,
                        inPrereservation = false

                    });
                }
            }
            string newPath = $"{DataStoragePath}/CinemaRoom{id}.json";
            seatingInstance.Add(seating);
            JsonAccess.UploadToJson(seatingInstance, newPath);
        }

    }
}