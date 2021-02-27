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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<int>> PostCustomer(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            return Ok(customer.CustomerId);
        }
    }
}
