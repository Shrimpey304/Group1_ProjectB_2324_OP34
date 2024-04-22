namespace Cinema;
using System.Net;
using System.Net.Mail;

public class EmailSenderLogic
{


    public void SendEmail(string recipientEmail, string subject, string body)
    {
        // Mail message
        MailMessage message = new MailMessage(EmailSenderModel._senderEmail, recipientEmail);
        message.Subject = subject;
        message.Body = body;

        // Configure SMTP client
        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
        smtpClient.EnableSsl = true;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new NetworkCredential(EmailSenderModel._senderEmail, EmailSenderModel._senderPassword);

        try
        {
            // Send email
            smtpClient.Send(message);
            Console.WriteLine("Email sent successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to send email. Error message: " + ex.Message);
        }
        finally
        {
            // Clean up resources
            message.Dispose();
            smtpClient.Dispose();
        }
    }
}
