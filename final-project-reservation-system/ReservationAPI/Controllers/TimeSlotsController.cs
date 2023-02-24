using Microsoft.AspNetCore.Mvc;
using ReservationAPI.DAOs;
using ReservationAPI.Interfaces;
using ReservationAPI.Models;

namespace ReservationAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TimeSlotsController : ControllerBase, ITimeSlotsController
{
    private readonly TimeSlotsDao _timeSlotsDao;

    public TimeSlotsController(TimeSlotsDao timeSlotsDao)
    {
        _timeSlotsDao = timeSlotsDao;
    }

    //CREATE
    [HttpPost]
    [Route("")]
    public async Task<IActionResult> CreateTimeSlot([FromRoute] string name, [FromBody] TimeSlotsRequest newTimeSlot)
    {
        try
        {
            await _timeSlotsDao.CreateTimeSlot(name, newTimeSlot);
            return StatusCode(201);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    //READ

    //UPDATE

    //DELETE

}   