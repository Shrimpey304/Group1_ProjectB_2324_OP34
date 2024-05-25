using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
namespace Cinema; 

public class UserRepository
{
    const string _filePath = @"DataStorage\Accounts.json";

    public List<AccountModel> GetAllUsers()
    {
        var jsonData = JsonAccess.ReadFromJson<AccountModel>(_filePath);
        return jsonData ?? new List<AccountModel>();
    }

    public void AddUser(AccountModel newUser)
    {
        if (UserExists(newUser.EmailAddress))
        {
            return;
        }

        var users = GetAllUsers();
        newUser.Id = users.Any() ? users.Max(u => u.Id) + 1 : 1; // Auto-increment ID
        users.Add(newUser);
        JsonAccess.UploadToJson(users, _filePath);

    }

    public bool UserExists(string email)
    {
        var users = GetAllUsers();
        return users.Any(u => u.EmailAddress == email);
    }
}
