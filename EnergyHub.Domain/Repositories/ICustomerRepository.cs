using EnergyHub.Domain.Entities.Customer;
using EnergyHub.Domain.Models.Token;

namespace EnergyHub.Domain.Repositories
{
    public interface ICustomerRepository
    {
        public Task<Customer> AddCustomerAsync(Customer customer);
        public Task<Customer> GetCustomerAsync(Guid Id);
        public Task<List<Customer>> GetCustomerListAsync();
        public Task<bool> UpdateCustomerTokenAsync(AuthenticationResponse response, Guid Id);

    }
}
