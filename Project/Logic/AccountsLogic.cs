using Cinema;

public class AccountsLogic
{
	public static List<AccountModel> _accounts = new List<AccountModel>();
	public static AccountModel? CurrentAccount { get; private set; }
	private const string filePathAccounts = @"DataStorage\Accounts.json";

	// public AccountsLogic()
	// {
	// 	// Initialize accounts list by reading from the JSON file
	// 	_accounts = JsonAccess.ReadFromJson<AccountModel>(filePathAccounts) ?? new List<AccountModel>();
	// }

	public AccountModel? CheckLogin(string email, string password)
	{
		try
		{
			// Attempt to find the user by email.
			_accounts = JsonAccess.ReadFromJson<AccountModel>(filePathAccounts);
			var user = _accounts.FirstOrDefault(u => u != null && u.EmailAddress.Equals(email, StringComparison.OrdinalIgnoreCase));
			if (user != null)
			{
				// Hash the input password with the user's stored salt
				var hashedInputPassword = PasswordHasher.HashPassword(password, user.Salt);

				// Compare the hashed input password with the stored hashed password
				if (hashedInputPassword == user.Password)
				{
					SetAllAccountsInactive();  // Set all accounts to inactive
					user.IsActive = true;      // Set the current user as active
					CurrentAccount = user;     // Update the current account pointer
					JsonAccess.UploadToJson(_accounts, filePathAccounts); // Save changes to JSON
					return user;
				}
			}else{
				throw new NullReferenceException("");
			}
			// Authentication failed
			return null;
		}
		catch (Exception e)
		{
			Console.WriteLine($"Error during login: {e}");
			return null;
		}
	}

	public static void SetAllAccountsInactive()
	{
		foreach (var account in _accounts)
		{
			account.IsActive = false;  // Set IsActive to false for all accounts
		}
		JsonAccess.UploadToJson<AccountModel>(_accounts, filePathAccounts);
	}

	public static void logout(){
		SetAllAccountsInactive();
		CurrentAccount = null;
		MenuUtils.displayMainMenu();
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

	public static void GetTickets(){

		Console.WriteLine("Current Tickets:\n\n");

		if (CurrentAccount != null){
			foreach(var ticket in CurrentAccount.TicketList){
				Console.WriteLine("-------------------------------------------------");
				Console.WriteLine($"Movie: {ticket.moviesession.MovieID} Time: {ticket.moviesession.StartTime}");
				Console.Write($"Seats (Row {ticket.ReservedSeats[0].Item1}): ");
				foreach (var seat in ticket.ReservedSeats){
					Console.Write($"{seat.Item2} ");
				}
				Console.WriteLine();
			}
			Console.WriteLine("-------------------------------------------------");
		}
	}

	public static void getuserinfo(){
		Console.WriteLine("your account info:");

		Console.WriteLine($"Name: {CurrentAccount.FullName}");
		Console.WriteLine($"EMail: {CurrentAccount.EmailAddress}");
	}
}
