namespace Cinema;

public class Program{

    public static void Main(){

        // const string fileName = "DataStorage/CinemaRoom1.json";
        // const string fileNameSesh = "DataStorage/Sessions.json";
        // // MovieSession.MakeSession(DateTime.Now, DateTime.Now.AddHours(1));
        // List<MovieSession> sessions = SessionsJsonUtils.ReadFromJson(fileNameSesh);
        // MovieSession sesh = sessions[0];
        // DisplayRoom.SelectSeating(fileName, sesh);
        LoginMenu.RunApplication(); // call LoginMenu
        
    }

}