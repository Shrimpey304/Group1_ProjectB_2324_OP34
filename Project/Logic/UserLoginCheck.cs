using Cinema;

namespace Cinema
{
    public class UserLoginCheck
    {
        private readonly Accountdataaccess _accountJsonUtils = new AccountJsonUtils();

        public bool ValidateLogin(string username, string password)
        {
            var users = _accountJsonUtils.GetUsers();
            return users.Exists(u => u.Username == username && u.Password == password);
        }

        public void RegisterUser(string username, string password)
        {
            var user = new User { Username = username, Password = password };
            _accountJsonUtils.SaveUser(user);
        }
    }
}
