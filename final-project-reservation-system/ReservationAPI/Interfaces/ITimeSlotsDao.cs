using ReservationAPI.DAOs;
using ReservationAPI.Models;

namespace ReservationAPI.Interfaces
{
    public interface ITimeSlotsDao
    {
        Task CreateTimeSlot(string name, TimeSlotsRequest newTimeSlot);

        Task<IEnumerable<TimeSlots>> GetTimeSlots();

        Task DeleteSpecificTimeSlots(string name, string slotStartTime);
        Task DeleteAllTimeSlotsByLocation(string name);
    }
}
