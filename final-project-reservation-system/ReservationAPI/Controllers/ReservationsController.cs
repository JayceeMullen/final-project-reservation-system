using Microsoft.AspNetCore.Mvc;
using ReservationAPI.DAOs;
using ReservationAPI.Interfaces;
using ReservationAPI.Models;

namespace ReservationAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class ReservationsController : ControllerBase
{
    private readonly ReservationsDao _reservationsDao;

    public ReservationsController(ReservationsDao reservationsDao)
    {
        _reservationsDao = reservationsDao;
    }
    
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetReservations()
    {
        try
        {
            IEnumerable<Reservation> reservations = await _reservationsDao.GetReservations();
            throw new Exception("This is a test exception");
            return Ok(reservations);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPost]
    [Route("")]
    public async Task<IActionResult> CreateReservation([FromBody] ReservationRequest reservationRequest)
    {
        try
        {
            await _reservationsDao.CreateReservation(reservationRequest);
            return StatusCode(201);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}