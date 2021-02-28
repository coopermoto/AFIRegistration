using AFIRegistration.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AFIRegistration.Service.Services
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<Customer> AddCustomerAsync(Customer newCustomer);
    }
}
