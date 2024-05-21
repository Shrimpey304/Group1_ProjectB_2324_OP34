namespace Cinema;
using Newtonsoft.Json;

public class JsonAccess{

    public static List<T> ReadFromJson<T>(string fileName) where T : class
    {
        if (File.Exists(fileName))
        {
            string json = File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject<List<T>>(json);
        }
        else
        {
            Console.WriteLine($"File not found: {fileName}");
            return new List<T>();
        }
    }

    public static void UploadToJson<T>(List<T> objList, string fileName) where T : class
    {
        string json = JsonConvert.SerializeObject(objList, Formatting.Indented);
        File.WriteAllText(fileName, json);
    }
}