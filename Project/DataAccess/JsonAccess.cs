namespace Cinema;
using Newtonsoft.Json;

public class JsonAccess{

    public static List<T> ReadFromJson<T>(string fileName) where T : class
    {
        if (File.Exists(fileName))
        {
            string json = File.ReadAllText(fileName);
            try{

                return JsonConvert.DeserializeObject<List<T>>(json)!;

            }catch(JsonSerializationException e){

                Console.WriteLine($"Error reading file: {fileName}");
                Console.WriteLine(e.Message);
                return new List<T>();

            }
        }
        else
        {
            Console.WriteLine($"File not found: {fileName}");
            return new List<T>();
        }
    }

    public static void UploadToJson<T>(List<T> objList, string fileName) where T : class
    {
        try{

            string json = JsonConvert.SerializeObject(objList, Formatting.Indented);
            File.WriteAllText(fileName, json);

        }catch(JsonSerializationException e){
            
            Console.WriteLine($"Error writing to file: {fileName}");
            Console.WriteLine(e.Message);
        }
    }
}