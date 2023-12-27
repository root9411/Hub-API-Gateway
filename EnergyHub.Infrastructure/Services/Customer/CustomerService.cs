using System;
using AutoMapper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using EnergyHub.Domain.Repositories;
using EnergyHub.Application.Services;
using EnergyHub.Application.Services.Customer;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using EnergyHub.Domain.Models.Token;

namespace EnergyHub.Infrastructure.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<EnergyHub.Domain.Entities.Customer.Customer> AddCustomerAsync(EnergyHub.Domain.Entities.Customer.Customer customer)
        {
            var result = await _customerRepository.AddCustomerAsync(customer);
            return result;
        }

        public async Task<EnergyHub.Domain.Entities.Customer.Customer> GetCustomerAsync(Guid Id)
        {
            var result = await _customerRepository.GetCustomerAsync(Id);
            return result;
        }

        public async Task<List<EnergyHub.Domain.Entities.Customer.Customer>> GetCustomerListAsync()
        {
            var result = await _customerRepository.GetCustomerListAsync();
            return result;
        }

        public async Task<bool> UpdateTokenInfo(AuthenticationResponse response, Guid Id, bool FirstToken)
        {
            var result = await _customerRepository.UpdateCustomerTokenAsync(response,Id,FirstToken);
            return result;
        }
    }
}
