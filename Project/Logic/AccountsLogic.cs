using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
namespace Cinema;


//This class is not static so later on we can use inheritance and interfaces
class AccountsLogic
{
    private List<AccountModel> _accounts;

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    static public AccountModel? CurrentAccount { get; private set; }

    const string filePathAccounts = @"DataStorage\Accounts.json";
    public AccountsLogic()
    {
        _accounts = JsonAccess.ReadFromJson<AccountModel>(filePathAccounts);
    }


    public void UpdateList(AccountModel acc)
    {

        JsonAccess.UpdateSingleObject<AccountModel>(acc ,filePathAccounts);
    }

    public AccountModel GetById(int id)
    {
        return _accounts.Find(i => i.Id == id)!;
    }

    public AccountModel CheckLogin(string email, string password)
    {
        if (email == null || password == null)
        {
            return null;
        }
        CurrentAccount = _accounts.Find(i => i.EmailAddress == email && i.Password == password);
        return CurrentAccount!;
    }
}