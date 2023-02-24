using ReservationAPI.Models;

namespace ReservationAPI.Interfaces
{
    public interface ILocationDao
    {
        Task CreateLocation(LocationRequest newLocation);
        Task<IEnumerable<Location>> GetLocation();
        Task<Location> GetLocationByName(string name);
        Task DeleteLocation(string name);
        Task UpdateLocationByName(string name, LocationRequest locationRequest);
        Task PatchLocationByName(string name, string? newName, int? newCapacity)

    }
}
