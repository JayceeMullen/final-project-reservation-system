using Microsoft.AspNetCore.Mvc;
using ReservationAPI.Models;

namespace ReservationAPI.Interfaces;
internal interface ICustomerController
    {
    Task<IActionResult> CreateCustomer([FromBody] Customer newCustomer);
    Task<IActionResult> GetCustomers();
    Task<IActionResult> GetCustomerByPhoneNumber([FromRoute] string phonenumber);
    Task<IActionResult> DeleteCustomer([FromRoute] string phonenumber);
    Task<IActionResult> UpdateCustomerByPhoneNumber([FromBody] Customer updateRequest);

    }