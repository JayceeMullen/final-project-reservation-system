using Moq;
using ReservationAPI.Interfaces;
using ReservationAPI.Models;

namespace ReservationAPITest.ReservationsDao;

[TestClass]
public class ReservationsDaoGetReservationsShould
{
    [TestMethod]
    public void ReturnAllReservations()
    {
        // Arrange
        var reservationsDao = new Mock<IReservationsDao>();

        // Act
        Task<IEnumerable<Reservation>> reservations = reservationsDao.Object.GetReservations();

        // Assert
        Assert.IsTrue(reservations.IsCompletedSuccessfully);
    }
}