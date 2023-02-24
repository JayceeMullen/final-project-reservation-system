using System.Data;
using Dapper;
using Microsoft.IdentityModel.Tokens;
using ReservationAPI.Interfaces;
using ReservationAPI.Models;

namespace ReservationAPI.DAOs;

public class ReservationsDao : IReservationsDao
{
    private readonly ReservationContext _context;

    public ReservationsDao(ReservationContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Reservation>> GetReservations()
    {
        const string query = "SELECT * FROM Reservations";
        using IDbConnection connection = _context.CreateConnection();
        IEnumerable<Reservation> reservations = await connection.QueryAsync<Reservation>(query);
        return reservations.ToList();
    }

    public async Task CreateReservation(ReservationRequest reservationRequest)
    {
        const string query =
            "INSERT INTO Reservations (LocationID, CustomerID, NumberOfGuests, ReservationDate)" 
            + " VALUES (@LocationID, @CustomerID, @NumberOfGuests, @Date)";
        
        using IDbConnection connection = _context.CreateConnection();
        var parameters = new DynamicParameters();
        parameters.Add("LocationID", reservationRequest.LocationId, DbType.Guid);
        parameters.Add("CustomerID", reservationRequest.CustomerId, DbType.Guid);
        parameters.Add("NumberOfGuests", reservationRequest.NumberOfPeople, DbType.Int32);
        parameters.Add("Date", reservationRequest.ReservationDate, DbType.DateTime);

        await connection.ExecuteAsync(query, parameters);
    }

    public async Task DeleteReservation(Guid id)
    {
        const string query = "DELETE FROM Reservations WHERE ReservationID = @Id";
        using IDbConnection connection = _context.CreateConnection();
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Guid);
        await connection.ExecuteAsync(query, parameters);
    }

    public async Task<Reservation> GetReservationsById(Guid id)
    {
        const string query = "SELECT * FROM Reservations WHERE ReservationID = @Id";
        using IDbConnection connection = _context.CreateConnection();
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Guid);
        var reservation = await connection.QueryFirstOrDefaultAsync<Reservation>(query, parameters);
        return reservation;
    }

    public async Task<IEnumerable<Reservation>> GetReservationsByCustomerId(Guid id)
    {
        const string query = "SELECT * FROM Reservations WHERE CustomerID = @Id";
        using IDbConnection connection = _context.CreateConnection();
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Guid);
        IEnumerable<Reservation>? reservations = await connection.QueryAsync<Reservation>(query, parameters);
        return reservations;
    }

    public async Task<IEnumerable<Reservation>> GetReservationsByLocationId(Guid id)
    {
        const string query = "SELECT * FROM Reservations WHERE LocationID = @Id";
        using IDbConnection connection = _context.CreateConnection();
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Guid);
        IEnumerable<Reservation> reservations = await connection.QueryAsync<Reservation>(query, parameters);
        return reservations.ToList();
    }

    public async Task UpdateReservation(Guid id, ReservationRequest reservationRequest)
    {
        Reservation? reservation = await GetReservationsById(id);
        if (reservation == null)
        {
            throw new Exception("Reservation not found");
        }
        const string query =
            "UPDATE Reservations SET LocationID = @LocationID, CustomerID = @CustomerID, NumberOfGuests = @NumberOfGuests,"
            +" ReservationDate = @Date WHERE ReservationID = @Id";
        
        using IDbConnection connection = _context.CreateConnection();
        var parameters = new DynamicParameters();
        parameters.Add("LocationID", reservationRequest.LocationId, DbType.Guid);
        parameters.Add("CustomerID", reservationRequest.CustomerId, DbType.Guid);
        parameters.Add("NumberOfGuests", reservationRequest.NumberOfPeople, DbType.Int32);
        parameters.Add("Date", reservationRequest.ReservationDate, DbType.DateTime);
        parameters.Add("Id", id, DbType.Guid);
    }
}