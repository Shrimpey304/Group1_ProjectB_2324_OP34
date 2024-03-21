using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Cinema
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class Accountdataaccess
    {
        // Use an absolute path for the file location
        private readonly string _filePath = @"C:\Users\aidan\OneDrive\Documenten\GitHub\Group1_ProjectB_2324_OP34\Project\DataStorage\Accounts.json";

        public List<User> GetUsers()
        {
            // Ensure the directory exists before trying to read the file
            var directory = Path.GetDirectoryName(_filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
                return new List<User>();
            }

            if (!File.Exists(_filePath))
            {
                return new List<User>();
            }

            var json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<User>>(json);
        }

        public void SaveUser(User user)
        {
            // Ensure the directory exists before trying to write the file
            var directory = Path.GetDirectoryName(_filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var users = GetUsers();
            users.Add(user);
            var json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }
    }
}
