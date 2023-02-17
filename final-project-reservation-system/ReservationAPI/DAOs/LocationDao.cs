﻿using System.Collections.Generic;
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

    public async Task<Location> GetLocationByName(string name)
    {
        var query = $"SELECT * FROM Locations WHERE Name LIKE '%{name}%'";
        using IDbConnection connection = _context.CreateConnection();
        {
            var location = await connection.QueryFirstOrDefaultAsync<Location>(query).ConfigureAwait(false);
            return location;
        }
    }

    //UPDATE

    public async Task UpdateLocationByName(string name, LocationRequest locationRequest)
    {
        Guid locationToUpdate = GetLocationByName(name).Result.LocationID;

        const string query = "UPDATE Locations SET Name = @Name, Capacity = @Capacity, OpenTime = @OpenTime, CloseTime = @CloseTime";
        using IDbConnection connection = _context.CreateConnection();
        var parameters = new DynamicParameters();

        parameters.Add("LocationID", locationToUpdate, DbType.Guid);
        parameters.Add("Name", locationRequest.Name, DbType.String);
        parameters.Add("Capacity", locationRequest.Capacity, DbType.Int16);
        parameters.Add("OpenTime", locationRequest.OpenTime, DbType.String);
        parameters.Add("CloseTime", locationRequest.CloseTime, DbType.String);

        await connection.ExecuteAsync(query, parameters);
    }

    //DELETE

    public async Task DeleteLocation(string name)
    {
        var query = $"DELETE FROM Locations WHERE Name LIKE %'{name}'%";
        using IDbConnection connection = _context.CreateConnection();
        {
            await connection.ExecuteAsync(query);
        }
    }
}