using System;
using System.Linq;
using System.Collections.Generic;

namespace Cinema;

public class MovieSessionLogic
{
    public static MovieSessionModel ListSessions(int UserInput)
    {
        var SessionList = JsonAccess.ReadFromJson<MovieSessionModel>("DataStorage/Sessions.json");
        bool HasSessions = SessionList.Any(session => session.MovieID == UserInput);

        if (HasSessions)
        {

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Upcoming sessions for this movie:");
                List<MovieSessionModel> usableSessions = new();

                Console.WriteLine($" _________________________________________________________________________");
                Console.WriteLine($"| {"Session".PadRight(7)} | {"Start".PadRight(25)} | {"End".PadRight(25)} | Room: |");
                Console.WriteLine($"|---------+---------------------------+---------------------------+-------|");

                int Counter = 1;
                foreach (MovieSessionModel session in SessionList.Where(s => s.MovieID == UserInput))
                {
                    Console.WriteLine($"| {Counter.ToString().PadRight(7)} | {session.StartTime.ToString().PadRight(25)} | End: {session.EndTime.ToString().PadRight(20)} | {session.RoomID.ToString().PadRight(5)} |");
                    usableSessions.Add(session);
                    Counter++;
                }

                Console.WriteLine($" ------------------------------------------------------------------------");
                Console.WriteLine("\nPlease select a session by typing the session ID and pressing enter.");

                string inp = InputHandler.ReadInputWithCancelLoggedIn("\nPlease select a session by typing the session ID and pressing enter. Press ESC to cancel.");
                if (inp == null) return null; // Handle cancellation

                if (int.TryParse(inp, out int intinp) && intinp >= 1 && intinp <= usableSessions.Count)
                {
                    return usableSessions[intinp - 1]; // Return the selected session
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a numeric session ID within the valid range.");
                }
            }
        }
        else
        {
            Console.WriteLine("There are currently no sessions planned for this movie.\nPerhaps a different movie piques your interest.");
            return null;
        }
    }

    public static void ListSessionsNoReservation(int UserInput)
    {
        var SessionList = JsonAccess.ReadFromJson<MovieSessionModel>("DataStorage/Sessions.json");
        if (SessionList.Any(session => session.MovieID == UserInput))
        {
            Console.WriteLine("Upcoming sessions for this movie:");
            Console.WriteLine($" _________________________________________________________________________");
            Console.WriteLine($"| {"Session".PadRight(7)} | {"Start".PadRight(25)} | {"End".PadRight(25)} | Room: |");
            Console.WriteLine($"|---------+---------------------------+---------------------------+-------|");

            int Counter = 1;
            foreach (MovieSessionModel session in SessionList.Where(s => s.MovieID == UserInput))
            {
                Console.WriteLine($"| {Counter.ToString().PadRight(7)} | {session.StartTime.ToString().PadRight(25)} | End: {session.EndTime.ToString().PadRight(20)} | {session.RoomID.ToString().PadRight(5)} |");
                Counter++;
            }

            Console.WriteLine($" ------------------------------------------------------------------------");

            // Added cancellation functionality
            InputHandler.ReadInputWithCancelLoggedIn("Press ESC to return to the menu.");
        }
        else
        {
            Console.WriteLine("There are currently no sessions planned for this movie.\nPerhaps a different movie piques your interest.");
        }
    }

    public static MovieSessionModel GetSessionByID(int movieID)
    {
        List<MovieSessionModel> _sessions = JsonAccess.ReadFromJson<MovieSessionModel>("DataStorage/Sessions.json");
        return _sessions.FirstOrDefault(a => a.sessionID == movieID)!;
    }
}
