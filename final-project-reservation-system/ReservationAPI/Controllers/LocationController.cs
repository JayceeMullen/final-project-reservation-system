using Castle.Core.Resource;
using Microsoft.AspNetCore.Mvc;
using ReservationAPI.DAOs;
using ReservationAPI.Interfaces;
using ReservationAPI.Models;

namespace ReservationAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationController : ControllerBase, ILocationController
{
    private readonly LocationDao _locationDao;

    public LocationController(LocationDao locationDao)
    {
        _locationDao = locationDao;
    }

    //CREATE
    [HttpPost]
    [Route("")]
    public async Task<IActionResult> CreateLocation([FromBody] LocationRequest newLocation)
    {
        try
        {
            await _locationDao.CreateLocation(newLocation);
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
    public async Task<IActionResult> GetLocation()
    {
        try
        {
            IEnumerable<Location> location = await _locationDao.GetLocation();
            return Ok(location);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    [Route("{name}")]
    public async Task<IActionResult> GetLocationByName([FromRoute] string name)
    {
        try
        {
            var location = await _locationDao.GetLocationByName(name);
            if (location == null)
            {
                return StatusCode(404);
            }
            return Ok(location);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    //UPDATE

    [HttpPut]
    [Route("/UpdateByName/{name}")]
    public async Task<IActionResult> UpdateLocationByName([FromRoute] string name, [FromBody] LocationRequest locationRequest)
    {
        try
        {
            await _locationDao.UpdateLocationByName(name, locationRequest);
            return StatusCode(204);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    //DELETE

    [HttpDelete]
    [Route("{name}")]
    public async Task<IActionResult> DeleteLocation([FromRoute] string name)
    {
        try
        {
            var location = await _locationDao.GetLocationByName(name);
            if (location == null)
            {
                return StatusCode(404);
            }
            await _locationDao.DeleteLocation(name);
            return StatusCode(200);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}