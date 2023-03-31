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
        bool isValidReservation = await ValidateReservation(reservationRequest);

        if (isValidReservation)
        {
            const string query =
                "INSERT INTO Reservations (LocationTimeSlotID, CustomerID, NumberOfGuests, ReservationDate)"
                + " VALUES (@LocationTimeSlotID, @CustomerID, @NumberOfGuests, @Date)";
            using IDbConnection connection = _context.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("LocationTimeSlotID", reservationRequest.LocationTimeSlotId, DbType.Guid);
            parameters.Add("CustomerID", reservationRequest.CustomerId, DbType.Guid);
            parameters.Add("NumberOfGuests", reservationRequest.NumberOfGuests, DbType.Int32);
            parameters.Add("Date", reservationRequest.ReservationDate, DbType.DateTime);

            await connection.ExecuteAsync(query, parameters);
        }
        else
        {
            throw new Exception("Reservation not valid! Please check the number of guests and the selected time slot");
        }
    }

    private async Task<bool> ValidateReservation(ReservationRequest reservationRequest)
    {
        const string validateQuery = "EXEC ValidateReservation @LocationTimeSlotID, @ReservationDate, @NumberOfGuests";
        using IDbConnection connection = _context.CreateConnection();
        var parameters = new DynamicParameters();
        parameters.Add("LocationTimeSlotID", reservationRequest.LocationTimeSlotId, DbType.Guid);
        parameters.Add("ReservationDate", reservationRequest.ReservationDate, DbType.DateTime);
        parameters.Add("NumberOfGuests", reservationRequest.NumberOfGuests, DbType.Int32);
        var result = await connection.QueryFirstOrDefaultAsync<bool>(validateQuery, parameters);
        return result;
    }

    public async Task DeleteReservation(Guid id)
    {
        const string query = "DELETE FROM Reservations WHERE ReservationID = @ReservationId";
        using IDbConnection connection = _context.CreateConnection();
        var parameters = new DynamicParameters();
        parameters.Add("ReservationId", id, DbType.Guid);
        await connection.ExecuteAsync(query, parameters);
    }

    public async Task<Reservation> GetReservationsById(Guid id)
    {
        const string query = "SELECT * FROM Reservations WHERE ReservationID = @ReservationId";
        using IDbConnection connection = _context.CreateConnection();
        var parameters = new DynamicParameters();
        parameters.Add("ReservationId", id, DbType.Guid);
        var reservation = await connection.QueryFirstOrDefaultAsync<Reservation>(query, parameters);
        return reservation;
    }

    public async Task<IEnumerable<Reservation>> GetReservationsByCustomerId(Guid id)
    {
        const string query = "SELECT * FROM Reservations WHERE CustomerID = @ReservationId";
        using IDbConnection connection = _context.CreateConnection();
        var parameters = new DynamicParameters();
        parameters.Add("ReservationId", id, DbType.Guid);
        IEnumerable<Reservation>? reservations = await connection.QueryAsync<Reservation>(query, parameters);
        return reservations;
    }

    public async Task<IEnumerable<Reservation>> GetReservationsByTimeSlotId(Guid id)
    {
        const string query = "SELECT * FROM Reservations WHERE LocationTimeSlotID = @ReservationId";
        using IDbConnection connection = _context.CreateConnection();
        var parameters = new DynamicParameters();
        parameters.Add("ReservationId", id, DbType.Guid);
        IEnumerable<Reservation> reservations = await connection.QueryAsync<Reservation>(query, parameters);
        return reservations.ToList();
    }
    
    public async Task<IEnumerable<Reservation>> GetReservationsByLocationId(Guid id)
    {
        //TODO: Implement
        throw new NotImplementedException();
    }

    public async Task UpdateReservation(Guid id, ReservationRequest reservationRequest)
    {
        Reservation? reservation = await GetReservationsById(id);
        if (reservation == null)
        {
            throw new Exception("Reservation not found");
        }
        const string query =
            "UPDATE Reservations SET LocationTimeSlotID = @LocationTimeSlotID, CustomerID = @CustomerID, NumberOfGuests = @NumberOfGuests,"
            +" ReservationDate = @Date WHERE ReservationID = @ReservationId";
        
        using IDbConnection connection = _context.CreateConnection();
        var parameters = new DynamicParameters();
        parameters.Add("LocationTimeSlotID", reservationRequest.LocationTimeSlotId, DbType.Guid);
        parameters.Add("CustomerID", reservationRequest.CustomerId, DbType.Guid);
        parameters.Add("NumberOfGuests", reservationRequest.NumberOfGuests, DbType.Int32);
        parameters.Add("Date", reservationRequest.ReservationDate, DbType.DateTime);
        parameters.Add("ReservationId", id, DbType.Guid);
    }
}