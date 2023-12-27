using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using EnergyHub.Domain.Models.Token;

namespace EnergyHub.Application.Services.Customer
{
    public interface ICustomerService
    {
        public Task<EnergyHub.Domain.Entities.Customer.Customer> AddCustomerAsync(EnergyHub.Domain.Entities.Customer.Customer customer);
        public Task<EnergyHub.Domain.Entities.Customer.Customer> GetCustomerAsync(Guid Id);
        public Task<List<EnergyHub.Domain.Entities.Customer.Customer>> GetCustomerListAsync();

        public Task<bool> UpdateTokenInfo(AuthenticationResponse response, Guid Id,bool FirstToken);
    }
}
