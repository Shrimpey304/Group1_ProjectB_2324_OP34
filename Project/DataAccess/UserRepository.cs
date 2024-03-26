using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Cinema; 

public class UserRepository
{
    private readonly string _filePath = @"C:\Users\ronan\OneDrive\Documenten\GitHub\Group1_ProjectB_2324_OP34\Project\DataStorage\Accounts.json";

    public List<AccountModel> GetAllUsers()
    {
        var jsonData = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<AccountModel>>(jsonData) ?? new List<AccountModel>();
    }

    public void AddUser(AccountModel newUser)
    {
        var users = GetAllUsers();
        newUser.Id = users.Any() ? users.Max(u => u.Id) + 1 : 1; // Auto-increment ID
        users.Add(newUser);
        var options = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(_filePath, JsonSerializer.Serialize(users, options));
    }
}
