using AFIRegistration.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AFIRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly RegistrationContext _context;

        public CustomersController(RegistrationContext context)
        {
            _context = context;
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

            await _context.Customers.AddAsync(customer);

            await _context.SaveChangesAsync();

            return Ok(customer.CustomerId);
        }
    }
}
