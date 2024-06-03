using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Cinema
{
    class CancellationUI
    {
        public static void ShowCancellation(Ticket ticket)
        {
            // Retrieve session details for the canceled reservation
            MovieSessionModel currentSession = JsonAccess.ReadFromJson<MovieSessionModel>("DataStorage/Sessions.json")
                .Where(s => s.sessionID == ticket.SessionID)
                .FirstOrDefault();

            // If session details are not found, exit the method
            if (currentSession == null)
            {
                Console.WriteLine("Error: Session details not found.");
                return;
            }

            // Retrieve movie title for the canceled reservation
            string currentMovieTitle = FindMovie(currentSession.MovieID);

            // Construct a string to represent the reserved seats
            string currentSeats = $"Seats (Row {ticket.ReservedSeats[0].Item1}): ";
            foreach (var seat in ticket.ReservedSeats)
            {
                currentSeats += $"[{seat.Item2}] ";
            }

            // Construct a string to represent the ordered snacks
            string currentSnacks;
            string HTMLSnacks;

            if (ticket.OrderedSnacks != null)
            {
                currentSnacks = "Snacks:\n";
                HTMLSnacks = "Snacks:<br>";
                foreach (Tuple<string, int> snack in ticket.OrderedSnacks)
                {
                    currentSnacks += $"• {snack.Item1} [{snack.Item2}x]\n";
                    HTMLSnacks += $"• {snack.Item1} [{snack.Item2}x]<br>";
                }
            }
            else
            {
                currentSnacks = "No Snacks\n";
                HTMLSnacks = "No Snacks<br>";
            }

            // Create cancellation email HTML body
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
                        .email-address {{
                            color: green;
                        }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <h1>Cinneville Rotterdam</h1>
                        </div>
                        <div class='content'>
                            <h1>Hello, <span class='email-address'>{AccountsLogic.CurrentAccount!.EmailAddress}</span>!</h1>
                            <p>Reservation ID: #{ticket.TicketID}</p>
                            <p>Movie: [{currentSession.MovieID}] {currentMovieTitle}</p>
                            <p>Room: #{currentSession.RoomID}</p>
                            <p>{currentSeats}<br></p>
                            <p>{HTMLSnacks}<br></p>
                            <p>Total price: €{ticket.TotalPrice},00<br></p>
                            <p>Your reservation has been cancelled!</p>
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

            // Send cancellation email
            EmailSenderLogic emailSender = new EmailSenderLogic();

            // SMTP server details and email details
            string smtpHost = "solidhorizons.net"; // SMTP server
            int smtpPort = 587; // SMTP port for SSL
            string smtpUser = "cineville@solidhorizons.net"; // SMTP username
            string smtpPass = "cineville123!"; // SMTP password
            string fromAddress = "cineville@solidhorizons.net"; // Your email address
            string toAddress = AccountsLogic.CurrentAccount!.EmailAddress; // Recipient email address
            string subject = $"Cancellation of Reservation #{ticket.TicketID} at Cineville"; // Email subject
            string body = htmlBody; // Cancellation email body

            // Send email
            emailSender.SendEmail(smtpHost, smtpPort, smtpUser, smtpPass, fromAddress, toAddress, subject, body);
        }

        const string filePathMovies = "DataStorage/Movies.json";
        private static string FindMovie(int movieID)
        {
            var movies = JsonAccess.ReadFromJson<MovieModel>(filePathMovies);
            foreach (MovieModel movie in movies)
            {
                if (movie.MovieID == movieID)
                {
                    return movie.Title;
                }
            }
            return "";
        }
    }
}
