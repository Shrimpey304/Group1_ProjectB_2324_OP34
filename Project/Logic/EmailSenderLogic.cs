namespace Cinema;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

// public class EmailSenderLogic
// {


// 	public void SendEmail(string recipientEmail, string subject, string body)
// 	{
		
// 		// Configure SMTP client
// 		SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
// 		{
// 			Port = 587,
// 			EnableSsl = true,
// 			Credentials = new NetworkCredential("cinevujoana@gmail.com", "cineville123!")
			
// 		};

// 		try
// 		{
// 			// Send email
// 			smtpClient.Send("cinevujoana@gmail.com", recipientEmail, subject, body);
// 			Console.WriteLine("Email sent successfully.");
// 		}
// 		catch (Exception ex)
// 		{
// 			Console.WriteLine("Failed to send email. Error message: " + ex.Message);
// 		}
// 		finally
// 		{
// 			// Clean up resources
// 			smtpClient.Dispose();
// 		}
// 	}
// }

public class EmailSenderLogic
{
	public void SendEmail(string smtpHost, int smtpPort, string smtpUser, string smtpPass, 
						  string fromAddress, string toAddress, string subject, string body)
	{
		try
		{
			// Ensure SSL/TLS is enabled and properly validated
			ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;
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
			SmtpClient smtpClient = new SmtpClient(smtpHost, smtpPort);
			smtpClient.Credentials = new NetworkCredential(smtpUser, smtpPass);
			smtpClient.EnableSsl = true; // Enable SSL/TLS

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