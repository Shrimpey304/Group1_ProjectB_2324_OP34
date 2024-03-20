using Newtonsoft.Json;

namespace Cinema
{
    public class Accounts
    {
        public int Id { get; set; }
        public string MailAddress { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }


        public Accounts(int id, string mailAddress, string password, bool isAdmin = false, string phoneNumber = default, string name = default)
        {
            Id = id;
            MailAddress = mailAddress;
            Password = password;
            IsAdmin = isAdmin;
            PhoneNumber = phoneNumber;
            Name = name;
        }



        public bool ValidateLogin(string enteredMailAddress, string enteredPassword)
        {
            return enteredMailAddress == MailAddress && enteredPassword == Password;
        }
    }
}
