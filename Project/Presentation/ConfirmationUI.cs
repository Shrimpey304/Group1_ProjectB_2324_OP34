namespace Cinema;

// THIS PROGRAM USES AN EMAIL SERVER OF 1 OF THE TEAMMEMBERS OF THIS PROJECT.
// PLEASE BE AWARE THAT THIS SERVER, THE HOSTING, AND THE SETUP OF IT MAY BE SUBJECT TO CHANGE AND MAY NOT WORK IN THE FUTURE DUE TO LACK OF CODE UPKEEP!

class ConfirmationUI
{
	public static void ShowConfirmation(Ticket ticket)
	{
		MovieSessionModel currentSession = JsonAccess.ReadFromJson<MovieSessionModel>("DataStorage/Sessions.json").Where(s => s.sessionID == ticket.SessionID).First();
		
		List<MovieModel> movies = JsonAccess.ReadFromJson<MovieModel>("DataStorage/Movies.json");
		string CurrentMovieTitle = "Title not found :(";
		foreach (var movie in movies)
		{
			if (movie.MovieID == currentSession.MovieID)
			{
				CurrentMovieTitle = movie.Title;
			}
		}
		
		string CurrentSeats = $"Seats (Row {ticket.ReservedSeats[0].Item1}): ";
		foreach (var seat in ticket.ReservedSeats)
		{
			CurrentSeats += $"[{seat.Item2}] ";
		}
		

		string CurrentSnacks;
		string HTMLSnacks;
		
		if (SnackMenuLogic.OrderedSnacks != null){
			CurrentSnacks = "Snacks:\n";
			HTMLSnacks = "Snacks:<br>";
			foreach (Tuple<string, int> snack in SnackMenuLogic.OrderedSnacks)
			{
				CurrentSnacks += $"• {snack.Item1} [{snack.Item2}x]\n";
				HTMLSnacks += $"• {snack.Item1} [{snack.Item2}x]<br>";
			}
		}else{
			CurrentSnacks = "No Snacks\n";
			HTMLSnacks = "No Snacks<br>";
		}

		string ConfirmationTextBody = $"Your reservation has been made!\n" +
			$"Reservation ID: #{ticket.TicketID}\n\n" +
			$"Movie: [{currentSession.MovieID}] {CurrentMovieTitle}\n" +
			$"Room: #{currentSession.RoomID}\n" +
			$"{CurrentSeats}\n\n" +
			$"{CurrentSnacks}\n\n" +
			$"Total price: €{ticket.TotalPrice},00\n\n" +
			"Please pay at the register\n\n"+
			"Thank you for your reservation!";

		Console.Clear();
		Console.WriteLine(ConfirmationTextBody);

		string actionLink = "https://hr.nl";
		string htmlBody = $@"
		<!DOCTYPE html>
		<html>
		<head>
			<style>
				body {{
					font-family: Arial, sans-serif;
					margin: 0;
					padding: 0;
					background-color: #f4f4f4;
				}}
				.container {{
					width: 100%;
					max-width: 600px;
					margin: 0 auto;
					background-color: #ffffff;
					box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
					border-radius: 8px;
					overflow: hidden;
				}}
				.header {{
					background-color: #4CAF50;
					color: white;
					padding: 20px;
					text-align: center;
				}}
				.content {{
					padding: 20px;
					line-height: 1.6;
					color: #333333;
				}}
				.content h1 {{
					color: #4CAF50;
				}}
				.content p {{
					margin: 0 0 10px;
				}}
				.content a {{
					color: inherit;
					text-decoration: none;
				}}
				.footer {{
					background-color: #f1f1f1;
					color: #888888;
					text-align: center;
					padding: 10px;
					font-size: 12px;
				}}
				.button {{
					display: inline-block;
					padding: 10px 20px;
					margin: 20px 0;
					color: #ffffff;
					background-color: #4CAF50;
					text-decoration: none;
					border-radius: 5px;
				}}
				.button:hover {{
					background-color: #45a049;
				}}
			</style>
		</head>
		<body>
			<div class='container'>
				<div class='header'>
					<h1>Cinneville Rotterdam</h1>
				</div>
				<div class='content'>
					<h1>Hello, {AccountsLogic.CurrentAccount!.EmailAddress}!</h1>
					<p>Reservation Number: #{ticket.TicketID}</p>
					<p>Movie: [{currentSession.MovieID}] {CurrentMovieTitle}</p>
					<p>Room: #{currentSession.RoomID}</p>
					<p>{CurrentSeats}<br></p>
					<p>{HTMLSnacks}<br></p>
					<p>Total price: €{ticket.TotalPrice},00<br></p>
					<p>Thank you for your reservation!</p>
					<a href='{actionLink}' class='button'>Visit website</a>
				</div>
				<div class='footer'>
					<p>&copy; 2024 Hogeschool Rotterdam. All rights reserved.</p>
					<p>Wijnhaven 103, Rotterdam</p>
				</div>
			</div>
		</body>
		</html>
		";

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