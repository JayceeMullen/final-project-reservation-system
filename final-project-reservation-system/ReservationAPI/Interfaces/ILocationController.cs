using Microsoft.AspNetCore.Mvc;
using ReservationAPI.Models;

namespace ReservationAPI.Interfaces;

internal interface ILocationController
    {
    Task<IActionResult> CreateLocation([FromBody] LocationRequest newLocation);
    Task<IActionResult> GetLocation();
    Task<IActionResult> GetLocationByName([FromRoute] string name);
    Task<IActionResult> DeleteLocation([FromRoute] string name);
    Task<IActionResult> UpdateLocationByName([FromRoute] string name, [FromBody] LocationRequest locationRequest);
    Task<IActionResult> PatchLocationByName([FromRoute] string name, [FromQuery] string? newName, [FromQuery] int? newCapacity);
    }
