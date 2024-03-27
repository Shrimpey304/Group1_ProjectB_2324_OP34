namespace Cinema; 

public static class UserRegistration
{
    public static void Register(string email, string password, string fullName)
    {
        UserRepository userRepository = new();
        AccountModel newUser = new AccountModel(email, password, fullName, false); // isAdmin is always false on registration
        userRepository.AddUser(newUser);
    }
}
