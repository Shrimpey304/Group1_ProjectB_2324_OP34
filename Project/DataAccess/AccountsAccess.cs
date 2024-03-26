using System.Text.Json;

static class AccountsAccess
{
    static string path = System.IO.Path.Combine(Environment.CurrentDirectory, @"C:\Users\aidan\OneDrive\Documenten\GitHub\Group1_ProjectB_2324_OP34\Project\DataStorage\Accounts.json");


    public static List<AccountModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<AccountModel>>(json);
    }


    public static void WriteAll(List<AccountModel> accounts)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(accounts, options);
        File.WriteAllText(path, json);
    }



}