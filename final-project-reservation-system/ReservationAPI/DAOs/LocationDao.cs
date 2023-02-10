using System.Collections.Generic;
using System.Data;
using Castle.Core.Resource;
using Dapper;
using ReservationAPI.Interfaces;
using ReservationAPI.Models;

namespace ReservationAPI.DAOs;
public class LocationDao : ILocationDao
{
    private readonly ReservationContext _context;

    public LocationDao(ReservationContext context)
    {
        _context = context;
    }

    //CREATE
    public async Task CreateLocation(Location newLocation)
    {
        const string query = "INSERT INTO Locations (LocationID, Name, Capacity, OpenTime, CloseTime) VALUES (@locationid, @name, @capacity, @opentime, @closetime)";
        
        using IDbConnection connection = _context.CreateConnection();

        var parameters = new DynamicParameters();
        parameters.Add("LocationID", newLocation.LocationID, DbType.Guid);
        parameters.Add("Name", newLocation.Name, DbType.String);
        parameters.Add("Capacity", newLocation.Capacity, DbType.Int16);
        parameters.Add("OpenTime", newLocation.OpenTime, DbType.Time);
        parameters.Add("CloseTime", newLocation.CloseTime, DbType.Time);

        await connection.ExecuteAsync(query, parameters);
    }
    //UPDATE

    //UPDATE

    //DELETE
}