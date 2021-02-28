using AFIRegistration.Data.Models;
using AFIRegistration.Service.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AFIRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<ActionResult<int>> PostCustomer(CustomerDTO customerDTO)
        {
            var customer = new Customer
            {
                FirstName = customerDTO.FirstName,
                Surname = customerDTO.Surname,
                PolicyNumber = customerDTO.PolicyNumber,
                DateOfBirth = customerDTO.DateOfBirth,
                EmailAddress = customerDTO.EmailAddress
            };

            await _customerService.AddCustomerAsync(customer);

            return Ok(customer.CustomerId);
        }
    }
}
