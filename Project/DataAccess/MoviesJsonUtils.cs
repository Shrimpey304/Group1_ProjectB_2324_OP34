namespace Cinema;
using Newtonsoft.Json;

public class MoviesJsonUtils{

    const string filePath = "DataStorage/Movies.json";

    public static void UpdateSingleObject(Movie toWrite, string fileName){
        List<Movie> objList = ReadFromJson(fileName);
        
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

    public static bool AreEqual(Movie obj1, Movie obj2)
    {
        // Serialize objects to JSON and compare the strings
        string json1 = JsonConvert.SerializeObject(obj1, Formatting.None);
        string json2 = JsonConvert.SerializeObject(obj2, Formatting.None);
        return json1 == json2;
    }

    public static List<Movie> ReadFromJson(string fileName){
        try{

            if (File.Exists(fileName)){

                string json = File.ReadAllText(fileName);
                return JsonConvert.DeserializeObject<List<Movie>>(json)!;

            }
        }catch (Exception ex){

            Console.WriteLine($"Error reading from JSON file: {ex.Message}");
        }

        return new List<Movie>();
    }

    public static void UploadToJson(List<Movie> objList, string fileName){
        try{

            string json = JsonConvert.SerializeObject(objList, Formatting.Indented);
            File.WriteAllText(fileName, json);

        }catch (Exception ex){

            Console.WriteLine($"Error uploading to JSON file: {ex.Message}");
        }
    }
}