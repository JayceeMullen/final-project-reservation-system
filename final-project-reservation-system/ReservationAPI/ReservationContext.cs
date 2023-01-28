using System.Data;
using Microsoft.Data.SqlClient;

namespace ReservationAPI;

public class ReservationContext
{
    private readonly string _connectionString;

    public ReservationContext(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("SqlConnection");
    }

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }
}