using System.Data;
using Dapper;
using ReservationAPI.Interfaces;
using ReservationAPI.Models;

namespace ReservationAPI.DAOs;
public class CustomerDao : ICustomerDao
{
    private readonly ReservationContext _context;

    public CustomerDao(ReservationContext context)
    {
        _context = context;
    }

    //CREATE
    public async Task CreateCustomer(CustomerRequest newCustomer)
    {
        const string query = "INSERT INTO Customers (CustomerID, Name, PhoneNumber, Email) VALUES (NEWID(), @Name, @PhoneNumber, @Email)";
        
        using IDbConnection connection = _context.CreateConnection();
        var parameters = new DynamicParameters();
        parameters.Add("Name", newCustomer.Name, DbType.String);
        parameters.Add("PhoneNumber", newCustomer.PhoneNumber, DbType.String);
        parameters.Add("Email", newCustomer.Email, DbType.String);

        await connection.ExecuteAsync(query, parameters);
    }
    //READ
    public async Task<IEnumerable<Customer>> GetCustomers()
    {
        const string query = "SELECT * FROM Customers";
        using IDbConnection connection = _context.CreateConnection();
        IEnumerable<Customer> customers = await connection.QueryAsync<Customer>(query);
        return customers.ToList();
    }
    public async Task<Customer> GetCustomerByPhoneNumber(string phoneNumber)
    {
        var query = $"SELECT * FROM Customers WHERE PhoneNumber  LIKE '%{phoneNumber}%' ";

        using IDbConnection connection = _context.CreateConnection();
        {
            var customer = await connection.QueryFirstOrDefaultAsync<Customer>(query).ConfigureAwait(false);
            return customer;
        }
    }

    //UPDATE

    public async Task UpdateCustomerByPhoneNumber(string phoneNumber, CustomerRequest customerRequest)
    {
        Guid customerToUpdate = GetCustomerByPhoneNumber(phoneNumber).Result.CustomerID;

        const string query = "UPDATE Customers SET Name = @Name, PhoneNumber = @PhoneNumber, Email = @Email WHERE CustomerID = @CustomerID";

        using IDbConnection connection = _context.CreateConnection();

        var parameters = new DynamicParameters();
        parameters.Add("CustomerID", customerToUpdate, DbType.Guid);
        parameters.Add("Name", customerRequest.Name, DbType.String);
        parameters.Add("PhoneNumber", customerRequest.PhoneNumber, DbType.String);
        parameters.Add("Email", customerRequest.Email, DbType.String);

        await connection.ExecuteAsync(query, parameters);
    }

    //DELETE

    public async Task DeleteCustomer(string phonenumber)
    {
        var query = $"DELETE FROM Customers WHERE PhoneNumber LIKE '%{phonenumber}%' ";

        using IDbConnection connection = _context.CreateConnection();
        {
            await connection.ExecuteAsync(query);
        }
    }
}
