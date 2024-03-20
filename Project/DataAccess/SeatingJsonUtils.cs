namespace Cinema;
using Newtonsoft.Json;

public class SeatingJsonUtils{

    const string filePath = "DataStorage/CinemaRoom1.json";

    public static void UpdateSingleObject(Seating toWrite, string fileName){
        List<Seating> objList = ReadFromJson(fileName);
        
        if (toWrite != null && objList != null){

            int index = objList.FindIndex(item => AreEqual(item, toWrite));
            
            if (index != -1){

                objList[index] = toWrite;

            }else{

                objList.Add(toWrite);
            }
            
            UploadToJson(objList, fileName);
            
        }else{

            Console.WriteLine("unable to locate json");

        }
    }

    public static bool AreEqual(Seating obj1, Seating obj2)
    {
        // Serialize objects to JSON and compare the strings
        string json1 = JsonConvert.SerializeObject(obj1, Formatting.None);
        string json2 = JsonConvert.SerializeObject(obj2, Formatting.None);
        return json1 == json2;
    }

    public static List<Seating> ReadFromJson(string fileName){
        try{

            if (File.Exists(fileName)){

                string json = File.ReadAllText(fileName);
                return JsonConvert.DeserializeObject<List<Seating>>(json)!;

            }
        }catch (Exception ex){

            Console.WriteLine($"Error reading from JSON file: {ex.Message}");
        }

        return new List<Seating>();
    }

    public static void UploadToJson(List<Seating> objList, string fileName){
        try{

            string json = JsonConvert.SerializeObject(objList, Formatting.Indented);
            File.WriteAllText(fileName, json);

        }catch (Exception ex){

            Console.WriteLine($"Error uploading to JSON file: {ex.Message}");
        }
    }
}