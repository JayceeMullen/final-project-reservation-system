using Microsoft.AspNetCore.Mvc;
using ReservationAPI.Models;

namespace ReservationAPI.Interfaces;

internal interface ILocationController
    {
    Task<IActionResult> CreateLocation([FromBody] LocationRequest newLocation);
    Task<IActionResult> GetLocation();

    }
