using Microsoft.AspNetCore.Mvc;
using ReservationAPI.DAOs;
using ReservationAPI.Interfaces;
using ReservationAPI.Models;

namespace ReservationAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class ReservationsController : ControllerBase, IReservationsController
{
    private readonly ReservationsDao _reservationsDao;

    public ReservationsController(ReservationsDao reservationsDao)
    {
        _reservationsDao = reservationsDao;
    }
    
    //Create

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
    
    //Read
    
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetReservations()
    {
        try
        {
            IEnumerable<Reservation> reservations = await _reservationsDao.GetReservations();
            return Ok(reservations);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    [Route("GetReservationById/{id:guid}")]
    public async Task<IActionResult> GetReservationById(Guid id)
    {
        try
        {
            Reservation reservation =  await _reservationsDao.GetReservationsById(id);
            return Ok(reservation);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    [Route("GetReservationsByCustomerId/{id:guid}")]
    public async Task<IActionResult> GetReservationsByCustomerId(Guid id)
    {
        try
        {
            IEnumerable<Reservation> reservation =  await _reservationsDao.GetReservationsByCustomerId(id);
            return Ok(reservation);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    [Route("GetReservationsByLocationId/{id:guid}")]
    public async Task<IActionResult> GetReservationsByLocationId(Guid id)
    {
        try
        {
            IEnumerable<Reservation> reservations =  await _reservationsDao.GetReservationsByLocationId(id);
            return Ok(reservations);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    [Route("GetReservationsByTimeSlotId/{id:guid}")]
    public async Task<IActionResult> GetReservationsByTimeSlotId(Guid id)
    {
        try
        {
            IEnumerable<Reservation> reservations =  await _reservationsDao.GetReservationsByTimeSlotId(id);
            return Ok(reservations);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    //Update
    
    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> UpdateReservationById([FromRoute] Guid id, ReservationRequest reservationRequest)
    {
        try
        {
            await _reservationsDao.UpdateReservation(id, reservationRequest);
            return StatusCode(204);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    //Delete
    
    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteReservation(Guid id)
    {
        try
        {
            await _reservationsDao.DeleteReservation(id);
            return StatusCode(204);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}