namespace Cinema;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

// THIS PROGRAM USES AN EMAIL SERVER OF 1 OF THE TEAMMEMBERS OF THIS PROJECT.
// PLEASE BE AWARE THAT THIS SERVER, THE HOSTING, AND THE SETUP OF IT MAY BE SUBJECT TO CHANGE AND MAY NOT WORK IN THE FUTURE DUE TO LACK OF CODE UPKEEP!


public class EmailSenderLogic
{
	public void SendEmail(string smtpHost, int smtpPort, string smtpUser, string smtpPass, 
						  string fromAddress, string toAddress, string subject, string body)
	{
		try
		{
			ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate!;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Failed to enable SSL/TLS. Error: {ex.Message}");
		}
		try{
			// Create a new MailMessage object
			MailMessage mail = new MailMessage();
			mail.From = new MailAddress(fromAddress);
			mail.To.Add(toAddress);
			mail.Subject = subject;
			mail.Body = body;
			mail.IsBodyHtml = true;

            // Create a new SmtpClient object
            SmtpClient smtpClient = new SmtpClient(smtpHost, smtpPort)
            {
                Credentials = new NetworkCredential(smtpUser, smtpPass),
                EnableSsl = true 
            };

            // Send the email
            smtpClient.Send(mail);
			Console.WriteLine("\n(This information has been sent to the email address you provided.)");
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Failed to send email. Error: {ex.Message}");
		}
	}

	// Callback used to validate the certificate in an SSL conversation
	public static bool ValidateServerCertificate(
		object sender,
		X509Certificate certificate,
		X509Chain chain,
		SslPolicyErrors sslPolicyErrors)
		
		{
		try{
			if (sslPolicyErrors == SslPolicyErrors.None)
			{
				return true;   // No errors, accept the certificate
			}
			
			Console.WriteLine("Certificate error: " + sslPolicyErrors);
			return false;  // Certificate is invalid, do not accept
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Failed to validate server certificate. Error: {ex.Message}");
			return false;
		}
	}
}