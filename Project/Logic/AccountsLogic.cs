using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;
using Cinema;

public class AccountsLogic
{
    private List<AccountModel> _accounts;
    public static AccountModel? CurrentAccount { get; private set; }
    private const string filePathAccounts = @"DataStorage\Accounts.json";

    public AccountsLogic()
    {
        // Initialize accounts list by reading from the JSON file
        _accounts = JsonAccess.ReadFromJson<AccountModel>(filePathAccounts) ?? new List<AccountModel>();
    }

    public AccountModel CheckLogin(string email, string password)
    {
        // Attempt to find the user by email.
        var user = _accounts.FirstOrDefault(u => u.EmailAddress.Equals(email, StringComparison.OrdinalIgnoreCase));
        if (user != null)
        {
            // Hash the input password with the user's stored salt
            var hashedInputPassword = PasswordHasher.HashPassword(password, user.Salt);

            // Compare the hashed input password with the stored hashed password
            if (hashedInputPassword == user.Password)
            {
                CurrentAccount = user; // Successfully authenticated
                return user;
            }
        }

        // Authentication failed
        return null;
    }

    public void UpdateList(AccountModel acc)
    {
        // Find the account in the list and update it
        var index = _accounts.FindIndex(a => a.Id == acc.Id);
        if (index != -1)
        {
            _accounts[index] = acc;
            JsonAccess.UploadToJson(_accounts, filePathAccounts); // Persist changes to JSON
        }
    }

    public AccountModel GetById(int id)
    {
        // Find and return the account by ID
        return _accounts.FirstOrDefault(a => a.Id == id);
    }
}
// all might