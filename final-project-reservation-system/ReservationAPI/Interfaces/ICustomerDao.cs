using ReservationAPI.Models;

namespace ReservationAPI.Interfaces
{
    public interface ICustomerDao
    {
        Task CreateCustomer(Customer newCustomer);
        Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer> GetCustomerByPhoneNumber(string phonenumber);
        Task DeleteCustomer(string phonenumber);

    }
}