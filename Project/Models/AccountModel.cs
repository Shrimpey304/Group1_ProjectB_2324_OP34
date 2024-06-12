using System.Text.Json.Serialization;
using Cinema;

public class AccountModel : IID
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
    public bool IsAdmin { get; set; }

    [JsonPropertyName("isActive")]
    public bool IsActive { get; set; } // New property to indicate if the user is currently logged in

    public AccountModel(string emailAddress, string password, string salt, string fullName, bool isAdmin, bool isActive)
    {
        EmailAddress = emailAddress;
        Password = password;
        Salt = salt;
        FullName = fullName;
        IsAdmin = isAdmin;
        IsActive = isActive;
    }
}
