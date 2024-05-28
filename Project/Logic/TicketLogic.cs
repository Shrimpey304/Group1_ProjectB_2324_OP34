namespace Cinema
{
    public class TicketLogic
    {
        private const string filePathReservations = @"DataStorage\Reservation.json";
        private static List<Ticket> _reservations = JsonAccess.ReadFromJson<Ticket>(filePathReservations) ?? new List<Ticket>();

        public static MovieSessionModel? selectedSession;
        public static List<Tuple<int, int>>? selectedSeating;
        public static double totalSeatPrice;

        public static void ReserveTicket()
        {
            int selectedMovieID = MovieUI.ListAllMovies();
            Console.WriteLine("\n\n");
            selectedSession = MovieSessionUI.ListSessions(selectedMovieID);
            selectedSeating = DisplayRoomUI.SelectSeating(selectedSession);
            totalSeatPrice = DisplayRoom.getSeatPricing(selectedSeating, selectedSession);
            MenuUtils.displaySnackOption();
        }

        public static void AddReservation()
        {
            // Assuming SnackMenuLogic holds the ordered snacks
            List<Tuple<string, int>> orderedSnacks = SnackMenuLogic.OrderedSnacks; 

            Ticket newTicket = new Ticket(
                selectedSession!.sessionID, 
                selectedSeating!, 
                totalSeatPrice + SnackMenuLogic.TotalCost, 
                orderedSnacks,
                AccountsLogic.CurrentAccount!.Id, 
                AccountsLogic.CurrentAccount!.Id
            );

            int currentRoomID = MovieLogic.getSession(newTicket.SessionID).RoomID;
            
            // This part of the code is commented out because it seems unnecessary
            // foreach (Ticket ticket in _reservations.Where(t => t.sessionID == newticket.sessionID))
            // {
            //     currentRoomID = JsonAccess.ReadFromJson<MovieSessionModel>("DataStorage/Sessions.json").Where(s => s.sessionID == ticket.sessionID).First().RoomID;
            // }
            
            Console.WriteLine($"movie: {newTicket.SessionID}\nRoom: {currentRoomID} Seats: {newTicket.ReservedSeats} ");
            
            _reservations.Add(newTicket);
            JsonAccess.UploadToJson(_reservations, filePathReservations);
            ConfirmationUI.ShowConfirmation(newTicket);
            Console.WriteLine("Press any key to return to the main menu");
            Console.ReadKey();
            MenuUtils.displayLoggedinMenu();
        }
    }
}
