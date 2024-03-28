using System.Text.Json.Serialization;

public class AccountModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("emailAddress")]
    public string EmailAddress { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }

    [JsonPropertyName("salt")]
    public string Salt { get; set; }

    [JsonPropertyName("fullName")]
    public string FullName { get; set; }

    [JsonPropertyName("isAdmin")]
    public bool IsAdmin { get; set; } = false; // Default to false

    // Assuming ID is handled elsewhere (e.g., by UserRepository), we don't include it in the constructor.
    public AccountModel(string emailAddress, string password, string salt, string fullName, bool isAdmin)
    {
        EmailAddress = emailAddress;
        Password = password; // This will now store the hashed password
        Salt = salt;
        FullName = fullName;
        IsAdmin = isAdmin;
    }
}
