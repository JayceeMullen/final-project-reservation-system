using System.Collections.Generic;
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
    public async Task CreateCustomer(Customer newCustomer)
    {
        const string query = "INSERT INTO Customers (CustomerID, Name, PhoneNumber, Email) VALUES (@customerid, @name, @phonenumber, @email)";
        using IDbConnection connection = _context.CreateConnection();
        var parameters = new DynamicParameters();
        parameters.Add("CustomerID", newCustomer.CustomerID, DbType.Guid);
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
    public async Task<Customer> GetCustomerByPhoneNumber(string phonenumber)
    {
        var query = $"SELECT * FROM Customers WHERE PhoneNumber  LIKE '%{phonenumber}%' ";

        using IDbConnection connection = _context.CreateConnection();
        {
            var customer = await connection.QueryFirstOrDefaultAsync<Customer>(query).ConfigureAwait(false);
            return customer;
        }
    }

    //UPDATE

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
