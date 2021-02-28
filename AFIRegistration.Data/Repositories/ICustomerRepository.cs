using AFIRegistration.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AFIRegistration.Data.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> GetCustomerByIdAsync(int id);

        Task<List<Customer>> GetAllCustomersAsync();
    }
}
