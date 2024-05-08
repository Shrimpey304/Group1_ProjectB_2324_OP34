namespace Cinema;

public class TicketLogic{

	private const string filePathReservations = @"DataStorage\Reservation.json";
	private static List<Ticket> _reservations = JsonAccess.ReadFromJson<Ticket>(filePathReservations) ?? new List<Ticket>();

	
	public static void AddReservation(Ticket ticket)
	{
		_reservations.Add(ticket);
		JsonAccess.UploadToJson(_reservations,filePathReservations);
	}

}