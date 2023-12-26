using MediatR;
using AutoMapper;
using EnergyHub.Application.Context;
using EnergyHub.Domain.Repositories;
using EnergyHub.Application.Services;
using EnergyHub.Application.Services.Customer;
using EnergyHub.Application.Services.Shared;

namespace EnergyHub.Application.Commands.Customer
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerRequest, Domain.Entities.Customer.Customer>
    {
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;
        private readonly ISharedService _sharedService;

        public CreateCustomerHandler(IMapper mapper, ICustomerService customerService, ISharedService sharedService)
        {
            _mapper = mapper;
            _customerService = customerService;
            _sharedService = sharedService;
        }

        public async Task<Domain.Entities.Customer.Customer> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
        {
            var newCustomer = _mapper.Map<Domain.Entities.Customer.Customer>(request);

            Guid newClientId = Guid.NewGuid();
            newCustomer.ClientId = newClientId;

            string encryptedSecret = _sharedService.EncryptText(newCustomer.ClientSecret); 

            var customer = new Domain.Entities.Customer.Customer()
            {
                FirstName = newCustomer.FirstName,
                LastName = newCustomer.LastName,
                ClientId = newCustomer.ClientId,
                ClientSecret = encryptedSecret,
                EnvironmentDatabaseName = newCustomer.EnvironmentDatabaseName,
                AccessToken = newCustomer.AccessToken,
                IsActive = newCustomer.IsActive,
                CreatedOn = newCustomer.CreatedOn,
                UpdatedOn = newCustomer.UpdatedOn
            };

            return await _customerService.AddCustomerAsync(customer);
        }
    }
}
