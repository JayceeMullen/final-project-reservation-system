using Microsoft.AspNetCore.Mvc;
using ReservationAPI.Models;

namespace ReservationAPI.Interfaces;

internal interface ITimeSlotsController
{
    Task<IActionResult> CreateTimeSlot([FromRoute] string name, [FromBody] TimeSlotsRequest newTimeSlot);
}