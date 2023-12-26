using MediatR;
using AutoMapper;
using EnergyHub.Domain.Repositories;
using EnergyHub.Application.Services;
using EnergyHub.Application.Services.Customer;

namespace EnergyHub.Application.Queries.Customer
{
    public class GetCustomerHandler : IRequestHandler<GetCustomerRequest, List<EnergyHub.Domain.Entities.Customer.Customer>>
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public GetCustomerHandler(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        public async Task<List<EnergyHub.Domain.Entities.Customer.Customer>> Handle(GetCustomerRequest request, CancellationToken cancellationToken)
        {
            string id = request.Id;
            string envDB = request.EnvironmentDbName;
            return await _customerService.GetCustomerListAsync();
        }
    }
}
