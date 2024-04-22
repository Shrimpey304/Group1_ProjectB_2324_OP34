namespace Cinema;
public class EmailSenderModel
{
	public static string _senderEmail;
	public static string _senderPassword;

	public EmailSenderModel(string senderEmail, string senderPassword)
	{
		_senderEmail = senderEmail;
		_senderPassword = senderPassword;
	}

}
