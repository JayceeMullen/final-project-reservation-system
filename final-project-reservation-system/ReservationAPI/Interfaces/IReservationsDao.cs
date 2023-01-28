using ReservationAPI.Models;

namespace ReservationAPI.Interfaces;

public interface IReservationsDao
{
    Task<IEnumerable<Reservation>> GetReservations();
    
    Task CreateReservation(ReservationRequest reservationRequest);
}