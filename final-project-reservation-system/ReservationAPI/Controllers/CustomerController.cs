using Microsoft.AspNetCore.Mvc;
using ReservationAPI.DAOs;
using ReservationAPI.Interfaces;
using ReservationAPI.Models;

namespace ReservationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class CustomerController : ControllerBase, ICustomerController
    {
        private readonly CustomerDao _customerDao;

        public CustomerController(CustomerDao customerDao)
        {
            _customerDao = customerDao;
        }

        //CREATE
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateCustomer([FromBody] Customer newCustomer)
        {
            try
            {
                await _customerDao.CreateCustomer(newCustomer);
                return StatusCode(201);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        //READ
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetCustomers()
        {
            try
            {
                IEnumerable<Customer> customers = await _customerDao.GetCustomers();
                return Ok(customers);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("{phoneNumber}")]
        public async Task<IActionResult> GetCustomerByPhoneNumber([FromRoute] string phoneNumber)
        {
            try
            {
                var customer = await _customerDao.GetCustomerByPhoneNumber(phoneNumber);
                if (customer == null)
                {
                    return StatusCode(404);
                }
                return Ok(customer);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        //UPDATE - CHECK THIS!

        [HttpPut]
        [Route("{phoneNumber}")]
        public async Task<IActionResult> UpdateCustomerByPhoneNumber([FromBody] Customer updateRequest)
        {
            try
            {
                var customer = await _customerDao.GetCustomerByPhoneNumber(updateRequest.PhoneNumber);
                if (customer == null)
                {
                    return StatusCode(404);
                }
                await _customerDao.UpdateCustomerByPhoneNumber(updateRequest);
                return StatusCode(204);
            }
            catch (Exception e) 
            {
            return StatusCode(500, e.Message);
            }
        }


        //DELETE

        [HttpDelete]
        [Route("{phoneNumber}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] string phoneNumber)
        {
            try
            {
                var customer = await _customerDao.GetCustomerByPhoneNumber(phoneNumber);
                if (customer == null)
                {
                    return StatusCode(404);
                }
                await _customerDao.DeleteCustomer(phoneNumber);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
