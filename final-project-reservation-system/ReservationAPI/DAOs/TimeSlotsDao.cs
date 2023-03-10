using System.Data;
using Dapper;
using ReservationAPI.Interfaces;
using ReservationAPI.Models;

namespace ReservationAPI.DAOs;
public class TimeSlotsDao : ITimeSlotsDao
{
    private readonly ReservationContext _context;
    private readonly ILocationDao _locationDao;
    public TimeSlotsDao(ReservationContext context, LocationDao locationDao)
    {
        _context = context;
        _locationDao = locationDao;
    }

    //CREATE
    public async Task CreateTimeSlot(string name, TimeSlotsRequest newTimeSlot)
    {
        Guid locationId = _locationDao.GetLocationByName(name).Result.LocationID;

        const string query = "INSERT INTO LocationTimeSlots (LocationTimeSlotID, LocationID, SlotStartTime) VALUES (NEWID(), @LocationID, @SlotStartTime)";

        using IDbConnection connection = _context.CreateConnection();

        var parameters = new DynamicParameters();

        parameters.Add("LocationID", locationId, DbType.Guid);
        parameters.Add("SlotStartTime", newTimeSlot.SlotStartTime, DbType.String) ;

        await connection.ExecuteAsync(query, parameters);
    }
    //READ

    public async Task<IEnumerable<TimeSlots>> GetTimeSlots()
    {
        const string query = "SELECT * FROM LocationTimeSlots";
        using IDbConnection connection = _context.CreateConnection();
        IEnumerable<TimeSlots> timeslots = await connection.QueryAsync<TimeSlots>(query);
        return timeslots.ToList();
    }

    //UPDATE

    //DELETE

    public async Task DeleteSpecificTimeSlots(string name, string slotStartTime)
    {
        Guid locationId = _locationDao.GetLocationByName(name).Result.LocationID;

        var query = $"DELETE FROM LocationTimeSlots WHERE LocationID LIKE '%{locationId}%' AND SlotStartTime LIKE '%{slotStartTime}%'";
        using IDbConnection connection = _context.CreateConnection();
        {
            await connection.ExecuteAsync(query);
        }
    }

    public async Task DeleteAllTimeSlotsByLocation(string name)
    {
        Guid locationId = _locationDao.GetLocationByName(name).Result.LocationID;

        var query = $"DELETE FROM LocationTimeSlots WHERE LocationID LIKE '%{locationId}%'";

        using IDbConnection connection = _context.CreateConnection();
        {
            await connection.ExecuteAsync(query);
        }
    }
}
