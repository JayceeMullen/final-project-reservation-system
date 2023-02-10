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
    public async Task CreateLocation(LocationRequest newLocation)
    {
        const string query = "INSERT INTO Locations (LocationID, Name, Capacity, OpenTime, CloseTime) VALUES (NEWID(), @Name, @Capacity, @OpenTime, @CloseTime)";
        
        using IDbConnection connection = _context.CreateConnection();

        var parameters = new DynamicParameters();

        parameters.Add("Name", newLocation.Name, DbType.String);
        parameters.Add("Capacity", newLocation.Capacity, DbType.Int16);
        parameters.Add("OpenTime", newLocation.OpenTime, DbType.String);
        parameters.Add("CloseTime", newLocation.CloseTime, DbType.String);

        await connection.ExecuteAsync(query, parameters);
    }
    //READ
    public async Task<IEnumerable<Location>> GetLocation()
    {
        const string query = "SELECT * FROM Locations";
        using IDbConnection connection = _context.CreateConnection();
        IEnumerable<Location> locations = await connection.QueryAsync<Location>(query);
        return locations.ToList();
    }

    //UPDATE

    //DELETE
}