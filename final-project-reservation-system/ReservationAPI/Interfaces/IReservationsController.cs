using Microsoft.AspNetCore.Mvc;
using ReservationAPI.Models;

namespace ReservationAPI.Interfaces;

public interface IReservationsController
{
    Task<IActionResult> GetReservations();
    Task<IActionResult> CreateReservation(ReservationRequest reservationRequest);
}