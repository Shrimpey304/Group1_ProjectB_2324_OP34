using Cinema; // Adjust namespace based on your project's structure

public static class UserRegistration
{
    public static void Register(string email, string password, string fullName)
    {
        var userRepository = new UserRepository();
        var newUser = new AccountModel(email, password, fullName, false); // isAdmin is always false on registration
        userRepository.AddUser(newUser);
    }
}
