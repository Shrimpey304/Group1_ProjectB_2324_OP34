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
                Console.WriteLine($"Error displaying seating IDs: {ex.Message}");
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
