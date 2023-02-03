using Moq;
using ReservationAPI.Interfaces;
using ReservationAPI.Models;

namespace ReservationAPITest.ReservationsDao;

[TestClass]
public class ReservationsDao_CreateReservationShould
{
    [TestMethod]
    public void CreateReservation()
    {
        // Arrange
        var reservationsDao = new Mock<IReservationsDao>();
        var reservationRequest = new ReservationRequest
        {
            CustomerId = new Guid(),   
            LocationId = new Guid(),
            NumberOfPeople = 1,
            ReservationStartDateTime = DateTime.Now,
            ReservationEndDateTime = DateTime.Now
        };

        // Act
        reservationsDao.Object.CreateReservation(reservationRequest);
        
        // Assert
        reservationsDao.Verify(x => x.CreateReservation(reservationRequest), Times.Once);
    }
}