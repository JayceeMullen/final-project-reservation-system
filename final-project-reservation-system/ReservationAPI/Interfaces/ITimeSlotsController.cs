using Microsoft.AspNetCore.Mvc;
using ReservationAPI.Models;

namespace ReservationAPI.Interfaces;

internal interface ITimeSlotsController
{
    Task<IActionResult> CreateTimeSlot([FromRoute] string name, [FromBody] TimeSlotsRequest newTimeSlot);

    Task<IActionResult> GetTimeSlots();

    Task<IActionResult> DeleteSpecificTimeSlots([FromRoute] string name, [FromBody] string slotStartTime);
    Task<IActionResult> DeleteAllTimeSlotsByLocation([FromRoute] string name);
    Task<IActionResult> GetTimeSlotsByLocation([FromRoute] string name);
}