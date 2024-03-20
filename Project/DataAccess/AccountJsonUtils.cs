using Newtonsoft.Json;

namespace Cinema
{
    
    public class AccountJsonUtils
    {
        const string filePath = "DataStorage/Accounts.json";

        public static void UpdateSingleObject(Accounts toWrite)
        {
            List<Accounts> objList = ReadFromJson(filePath);

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

                UploadToJson(objList);
            }
            else
            {
                Console.WriteLine("Unable to locate JSON.");
            }
        }

        public static bool AreEqual(Accounts obj1, Accounts obj2)
        {
            string json1 = JsonConvert.SerializeObject(obj1, Formatting.None);
            string json2 = JsonConvert.SerializeObject(obj2, Formatting.None);
            return json1 == json2;
        }

        public static List<Accounts> ReadFromJson(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    string json = File.ReadAllText(fileName);
                    return JsonConvert.DeserializeObject<List<Accounts>>(json) ?? new List<Accounts>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading from JSON file: {ex.Message}");
            }

            return new List<Accounts>();
        }

        public static void UploadToJson(List<Accounts> objList)
        {
            try
            {
                string json = JsonConvert.SerializeObject(objList, Formatting.Indented);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error uploading to JSON file: {ex.Message}");
            }
        }
    }
}
