using Microsoft.AspNetCore.Mvc;
using ReservationAPI.Models;

namespace ReservationAPI.Interfaces;

public interface IReservationsController
{
    Task<IActionResult> GetReservations();
    Task<IActionResult> CreateReservation(ReservationRequest reservationRequest);
    
    Task<IActionResult> GetReservationById(Guid id);
    
    Task<IActionResult> GetReservationsByCustomerId(Guid id);
    
    Task<IActionResult> GetReservationsByLocationId(Guid id);

    Task<IActionResult> UpdateReservationById(Guid id, ReservationRequest reservationRequest);
    
    Task<IActionResult> DeleteReservation(Guid id);
    
}