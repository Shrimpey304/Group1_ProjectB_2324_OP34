namespace Cinema;
using Newtonsoft.Json;

public class JsonAccess{

    const string filePathMovies = "DataStorage/Movies.json";
    const string filePathAccounts = @"DataStorage\Accounts.json";
    const string filePathCineR1 = "DataStorage/CinemaRoom1.json";
    const string filePathSessions = "DataStorage/Sessions.json";

        public static void UpdateSingleObject<T>(T toWrite, string fileName) where T : class
    {
        List<T> objList = ReadFromJson<T>(fileName);
        if (toWrite != null && objList != null)
        {
            int index = objList.FindIndex(item => AreEqual(item, toWrite));
            if (index != -1)
            {
                objList[index] = toWrite;
            }
            else
            {
                objList.Add(toWrite);
            }
            UploadToJson(objList, fileName);
        }
        else
        {
            Console.WriteLine("Unable to locate JSON or object to write is null.");
        }
    }

    private static bool AreEqual<T>(T obj1, T obj2)
    {
        string json1 = JsonConvert.SerializeObject(obj1, Formatting.None);
        string json2 = JsonConvert.SerializeObject(obj2, Formatting.None);
        return json1 == json2;
    }

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