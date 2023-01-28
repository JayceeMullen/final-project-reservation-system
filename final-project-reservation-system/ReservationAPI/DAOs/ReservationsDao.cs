using System.Data;
using Dapper;
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
            "INSERT INTO Reservations (LocationID, CustomerID, NumberOfGuests, ReservationStartTime, ReservationEndTime)" 
            + " VALUES (@LocationID, @CustomerID, @NumberOfGuests, @Start, @End)";
        
        using IDbConnection connection = _context.CreateConnection();
        var parameters = new DynamicParameters();
        parameters.Add("LocationID", reservationRequest.LocationId, DbType.Int32);
        parameters.Add("CustomerID", reservationRequest.CustomerId, DbType.Int32);
        parameters.Add("NumberOfGuests", reservationRequest.NumberOfPeople, DbType.Int32);
        parameters.Add("Start", reservationRequest.ReservationStartDateTime, DbType.DateTime);
        parameters.Add("End", reservationRequest.ReservationEndDateTime, DbType.DateTime);
        
        await connection.ExecuteAsync(query, parameters);
    }
}