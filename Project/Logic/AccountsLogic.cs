using Cinema;

public class AccountsLogic
{
	public static List<AccountModel> _accounts = new List<AccountModel>();
	public static AccountModel? CurrentAccount { get; private set; }
	private const string filePathAccounts = @"DataStorage\Accounts.json";


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
			Console.WriteLine("Error: " + e.Message);
			return null;
		}
	}

	public static void SetAllAccountsInactive()
	{
		foreach (var account in _accounts)
		{
			account.IsActive = false;  // Set IsActive to false for all accounts
		}
		JsonAccess.UploadToJson(_accounts, filePathAccounts);
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
		return _accounts.FirstOrDefault(a => a.Id == id)!;
	}

	public static void GetTickets()
	{

		Console.WriteLine("Current Tickets:\n\n");

		if (CurrentAccount != null)
		{
			
			List<Ticket> TicketsForAccount = JsonAccess.ReadFromJson<Ticket>("DataStorage/Reservation.json").Where(t => t.AccountID == CurrentAccount.Id).ToList();
			foreach(Ticket ticket in TicketsForAccount)
			{
				MovieSessionModel session = JsonAccess.ReadFromJson<MovieSessionModel>("DataStorage/Sessions.json").Where(s => s.sessionID == ticket.SessionID).First();
				Console.WriteLine("-------------------------------------------------");
				Console.WriteLine($"Room: {session.RoomID}");
				Console.WriteLine($"MovieID: {session.MovieID}\nTime: {session.StartTime}");
				Console.Write($"Seats (Row {ticket.ReservedSeats[0].Item1}): ");
				

				foreach (var seat in ticket.ReservedSeats)
				{
					Console.Write($"{seat.Item2} ");
				}
				Console.WriteLine();
				if (ticket.OrderedSnacks.Count != 0)
				{
				Console.WriteLine(" ______________________________________________");
				Console.WriteLine("| Name                                | Amount |");
				Console.WriteLine("|-------------------------------------+--------|");

				foreach (var snack in ticket.OrderedSnacks)
				{
					Console.WriteLine($"| {snack.Item1.PadRight(35)} | {snack.Item2.ToString("0.00").PadLeft(6)} |");
				}

				Console.WriteLine("_______________________________________________");
			
				Console.WriteLine();
				}

			}
			Console.WriteLine("-------------------------------------------------");
		}
	}
	

	public static void getuserinfo(){
		Console.WriteLine("your account info:");

		Console.WriteLine($"Name: {CurrentAccount!.FullName}");
		Console.WriteLine($"EMail: {CurrentAccount.EmailAddress}");
	}
}
