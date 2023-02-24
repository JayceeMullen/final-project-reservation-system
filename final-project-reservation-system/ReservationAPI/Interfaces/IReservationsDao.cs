using ReservationAPI.Models;

namespace ReservationAPI.Interfaces;

public interface IReservationsDao
{
    Task<IEnumerable<Reservation>> GetReservations();
    
    Task CreateReservation(ReservationRequest reservationRequest);
    
    Task DeleteReservation(Guid id);
    
    Task<Reservation> GetReservationsById(Guid id);
    
    Task<IEnumerable<Reservation>> GetReservationsByCustomerId(Guid id);
    
    Task<IEnumerable<Reservation>> GetReservationsByLocationId(Guid id);

    Task UpdateReservation(Guid id, ReservationRequest reservationRequest);
}