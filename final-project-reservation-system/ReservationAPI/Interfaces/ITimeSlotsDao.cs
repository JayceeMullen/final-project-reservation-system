using ReservationAPI.DAOs;
using ReservationAPI.Models;

namespace ReservationAPI.Interfaces
{
    public interface ITimeSlotsDao
    {
        Task CreateTimeSlot(string name, TimeSlotsRequest newTimeSlot);
    }
}
