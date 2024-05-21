namespace Cinema;

class ConfirmationLogic
{
	public static void ShowConfirmation(Ticket ticket)
	{
		List<MovieModel> movies = JsonAccess.ReadFromJson<MovieModel>("DataStorage/Movies.json");
		string CurrentMovieTitle = "Title not found :(";
		foreach (var movie in movies)
		{
			if (movie.MovieID == ticket.moviesession.MovieID)
			{
				CurrentMovieTitle = movie.Title;
			}
		}
		
		string CurrentSeats = $"Seats (Row {ticket.ReservedSeats[0].Item1}): ";
		foreach (var seat in ticket.ReservedSeats)
		{
			CurrentSeats += $"[{seat.Item2}] ";
		}
		
		string CurrentSnacks = "Snacks:\n";
		string HTMLSnacks = "Snacks:<br>";
		foreach (Tuple<string, int> snack in SnackMenuLogic.OrderedSnacks)
		{
			CurrentSnacks += $"• {snack.Item1} [{snack.Item2}x]\n";
			HTMLSnacks += $"• {snack.Item1} [{snack.Item2}x]<br>";
		}


		
		string ConfirmationTextBody = $"Your reservation has been made!\n" +
			$"Reservation ID: #{ticket.TicketID}\n\n" +
			$"Movie: [{ticket.moviesession.MovieID}] {CurrentMovieTitle}\n" +
			$"Room: #{ticket.moviesession.RoomID}\n" +
			$"{CurrentSeats}\n\n" +
			$"{CurrentSnacks}\n\n" +
			$"Total price: €{ticket.Totalprice},00\n\n" +
			"Please pay at the register\n\n"+
			"Thank you for your reservation!";

		Console.Clear();
		Console.WriteLine(ConfirmationTextBody);
		
		string htmlBody = $"<html>" +
			$"<body>" +
			$"<h1>Your reservation has been made!</h1>" +
			$"<p>Reservation ID: #{ticket.TicketID}<br></p>" +
			$"<p>Movie: [{ticket.moviesession.MovieID}] {CurrentMovieTitle}</p>" +
			$"<p>Room: #{ticket.moviesession.RoomID}</p>" +
			$"<p>{CurrentSeats}<br></p>" +
			$"<p>{HTMLSnacks}<br></p>" +
			$"<p>Total price: €{ticket.Totalprice},00<br></p>" +
			$"<p>Thank you for your reservation!</p>" +
			$"</body>" +
			$"</html>";

		// Send confirmation email
		EmailSenderLogic emailSender = new EmailSenderLogic();

		// SMTP server details and email details
		string smtpHost = "solidhorizons.net"; // SMTP server
		int smtpPort = 587; // SMTP port for SSL
		string smtpUser = "cineville@solidhorizons.net"; // SMTP username
		string smtpPass = "cineville123!"; // SMTP password
		string fromAddress = "cineville@solidhorizons.net"; // Your email address
		//string toAddress = "RammusShenCho@gmail.com"; // Basic email address
		
		//uncomment this and Comment the previous line to send the email to the user instead of the Basic mail
		string toAddress = AccountsLogic.CurrentAccount!.EmailAddress; // Recipient email address
		
		string subject = $"Your reservation at Cineville"; // Email subject
		string body = htmlBody; // Confirmation email body

		// Send email
		emailSender.SendEmail(smtpHost, smtpPort, smtpUser, smtpPass, fromAddress, toAddress, subject, body);
	}
}