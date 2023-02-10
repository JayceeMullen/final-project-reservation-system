using ReservationAPI.Models;

namespace ReservationAPI.Interfaces
{
    public interface ILocationDao
    {
        Task CreateLocation(Location newLocation);
    }
}
