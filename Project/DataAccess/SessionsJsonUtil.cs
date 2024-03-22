namespace Cinema;
using Newtonsoft.Json;

public class SessionsJsonUtils{

    const string filePath = "DataStorage/Sessions.json";

    public static void UpdateSingleObject(MovieSession toWrite, string fileName){
        List<MovieSession> objList = ReadFromJson(fileName);
        
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

    public static bool AreEqual(MovieSession obj1, MovieSession obj2)
    {
        // Serialize objects to JSON and compare the strings
        string json1 = JsonConvert.SerializeObject(obj1, Formatting.None);
        string json2 = JsonConvert.SerializeObject(obj2, Formatting.None);
        return json1 == json2;
    }

    public static List<MovieSession> ReadFromJson(string fileName){
        try{

            if (File.Exists(fileName)){

                string json = File.ReadAllText(fileName);
                return JsonConvert.DeserializeObject<List<MovieSession>>(json)!;

            }
        }catch (Exception ex){

            Console.WriteLine($"Error reading from JSON file: {ex.Message}");
        }

        return new List<MovieSession>();
    }

    public static void UploadToJson(List<MovieSession> objList, string fileName){
        try{

            string json = JsonConvert.SerializeObject(objList, Formatting.Indented);
            File.WriteAllText(fileName, json);

        }catch (Exception ex){

            Console.WriteLine($"Error uploading to JSON file: {ex.Message}");
        }
    }
}