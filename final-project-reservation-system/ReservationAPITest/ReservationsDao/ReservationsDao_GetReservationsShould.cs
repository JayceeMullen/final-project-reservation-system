using Moq;
using ReservationAPI.Interfaces;
using ReservationAPI.Models;

namespace ReservationAPITest.ReservationsDao;

[TestClass]
public class ReservationsDao_GetReservationsShould
{
    [TestMethod]
    public void ReturnAllReservations()
    {
        // Arrange
        var reservationsDao = new Mock<IReservationsDao>();
        var reservation = new Reservation
        {
            LocationId = new Guid(),
            CustomerId = new Guid(),
            NumberOfPeople = 1,
            ReservationDate = new DateTime(2021, 1, 1)
        };

        // Act
        Task<IEnumerable<Reservation>> reservations = reservationsDao.Object.GetReservations();

        // Assert
        Assert.IsTrue(reservations.IsCompletedSuccessfully);
    }
}