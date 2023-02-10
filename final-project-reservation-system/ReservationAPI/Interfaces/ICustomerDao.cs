using ReservationAPI.Models;

namespace ReservationAPI.Interfaces
{
    public interface ICustomerDao
    {
        Task CreateCustomer(CustomerRequest newCustomer);
        Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer> GetCustomerByPhoneNumber(string phonenumber);
        Task DeleteCustomer(string phonenumber);
        Task UpdateCustomerByPhoneNumber(string phoneNumber, CustomerRequest customerRequest);

    }
}