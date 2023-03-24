using Microsoft.AspNetCore.Mvc;
using ReservationAPI.DAOs;
using ReservationAPI.Interfaces;
using ReservationAPI.Models;
using System.Data;

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
    [Route("{name}")]
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
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetTimeSlots()
    {
        try
        {
            IEnumerable<TimeSlots> timeslots = await _timeSlotsDao.GetTimeSlots();
            return Ok(timeslots);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    [Route("GetTimeSlotsByLocation/{name}")]
    public async Task<IActionResult> GetTimeSlotsByLocation([FromRoute] string name)
    {
        try
        {
            IEnumerable<TimeSlots> timeslotsbylocation = await _timeSlotsDao.GetTimeSlotsByLocation(name);
            if (name == null)
            {
                return StatusCode(404);
            }
            return Ok(timeslotsbylocation);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    //UPDATE

    //DELETE

    [HttpDelete]
    [Route("DeleteSpecificTimeSlots/{name}/{slotStartTime}")]
    public async Task<IActionResult> DeleteSpecificTimeSlots([FromRoute] string name, string slotStartTime)
    {
        try
        {
            if (name == null)
            {
                return StatusCode(404);
            }
            await _timeSlotsDao.DeleteSpecificTimeSlots(name, slotStartTime);
            return StatusCode(200);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete]
    [Route("DeleteAllTimeSlotsByLocation/{name}")]
    public async Task<IActionResult> DeleteAllTimeSlotsByLocation([FromRoute] string name)
    {
        try
        {
            if (name == null)
            {
                return StatusCode(404);
            }
            await _timeSlotsDao.DeleteAllTimeSlotsByLocation(name);
            return StatusCode(200);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}   