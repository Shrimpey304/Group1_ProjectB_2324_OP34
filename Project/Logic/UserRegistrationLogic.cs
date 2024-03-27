namespace Cinema; 

public static class UserRegistrationLogic
{
    public static void Register(string email, string password, string fullName)
    {
        UserRepository userRepository = new();
        if (!userRepository.UserExists(email))
        {
            var salt = PasswordHasher.GenerateSalt();
            var hashedPassword = PasswordHasher.HashPassword(password, salt);
            
            AccountModel newUser = new AccountModel(email, hashedPassword, salt, fullName, false);
            userRepository.AddUser(newUser);
            Console.WriteLine("Registration successful. Welcome, " + fullName);
        }
        else
        {
            Console.WriteLine("A user with this email already exists.");
        }
    }
}
