using AFIRegistration.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AFIRegistration.Data.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(RegistrationContext registrationContext)
            : base(registrationContext)
        {
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await GetAll().FirstOrDefaultAsync(x => x.CustomerId == id);
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await GetAll().ToListAsync();
        }
    }
}
