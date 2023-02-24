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
        parameters.Add("SlotStartTime", newTimeSlot.SlotStartTime, DbType.String);

        await connection.ExecuteAsync(query, parameters);
    }
    //READ

    //UPDATE

    //DELETE

}
