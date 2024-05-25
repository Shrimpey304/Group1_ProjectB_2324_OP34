using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Cinema;


// WERKT NIET OP HET MOMENT!!!!!


namespace Cinema.Tests
{
    [TestClass]
    public class DisplayRoomTests
    {
        [TestMethod]
        public void SelectSeating_ReturnsNonNullList_WhenSeatsAreSelected()
        {
            // Arrange
            List<MovieSessionModel> sessions = JsonAccess.ReadFromJson<MovieSessionModel>("sessions.json");

            // Select a session for testing
            MovieSessionModel session = sessions.FirstOrDefault();

            // Act
            List<Tuple<int, int>> selectedSeats = DisplayRoom.SelectSeating(session);

            // Assert
            Assert.IsNotNull(selectedSeats);
        }

        [TestMethod]
        public void SelectSeating_ReturnsNull_WhenNoSeatsAreSelected()
        {
            // Arrange
            List<MovieSessionModel> sessions = JsonAccess.ReadFromJson<MovieSessionModel>("sessions.json");

            // Select a session for testing
            MovieSessionModel session = sessions.FirstOrDefault();

            // Act
            List<Tuple<int, int>> selectedSeats = DisplayRoom.SelectSeating(session);

            // Assert
            Assert.IsNull(selectedSeats);
        }

        // Add more test methods to cover other scenarios and edge cases
    }
}