using ReservationAPI.Models;

namespace ReservationAPI.Interfaces
{
    public interface ILocationDao
    {
        Task CreateLocation(LocationRequest newLocation);
        Task<IEnumerable<Location>> GetLocation();

    }
}
