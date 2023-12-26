using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using EnergyHub.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using EnergyHub.Application.Context;
using EnergyHub.Domain.Entities.Customer;
using EnergyHub.Domain.Models.Token;

namespace EnergyHub.Infrastructure.Repositories.Customer
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IEnergyHubDbContext _energyHubDbContext;

        public CustomerRepository(IEnergyHubDbContext energyHubDbContext)
        {
            _energyHubDbContext = energyHubDbContext;
        }

        public async Task<EnergyHub.Domain.Entities.Customer.Customer> AddCustomerAsync(EnergyHub.Domain.Entities.Customer.Customer customer)
        {
            var result = _energyHubDbContext.Customers.Add(customer);
            await _energyHubDbContext.SaveToDbAsync();
            return result.Entity;
        }

        public async Task<EnergyHub.Domain.Entities.Customer.Customer> GetCustomerAsync(Guid Id)
        {
            var result = await _energyHubDbContext.Customers.FindAsync(Id);
            return result;
        }

        public async Task<List<EnergyHub.Domain.Entities.Customer.Customer>> GetCustomerListAsync()
        {
            var result = _energyHubDbContext.Customers;
            return await result.ToListAsync();
        }

        public async Task<bool> UpdateCustomerTokenAsync(AuthenticationResponse response , Guid Id)
        {
            var data = await GetCustomerAsync(Id);
            if (data != null)
            {
                data.RefreshToken = response.RefreshToken;
                data.accessTokenExpirationTime = response.accessTokenExpirationTime;

                await _energyHubDbContext.SaveToDbAsync();

                return true;
            }

            return false;
        }
        
    }
}
