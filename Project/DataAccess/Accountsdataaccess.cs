using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace DataAccess
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class AccountJsonUtils
    {
        private readonly string _filePath = "DataStorage/Accounts.json"; // Adjusted path to reach the DataStorage folder

        public List<User> GetUsers()
        {
            if (!File.Exists(_filePath))
            {
                return new List<User>();
            }

            var json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<User>>(json);
        }

        public void SaveUser(User user)
        {
            var users = GetUsers();
            users.Add(user);
            var json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }
    }
}
