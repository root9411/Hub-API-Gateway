using AutoMapper;

namespace EnergyHub.Application.Commands.Customer
{
    public class CreateCustomerMapper : Profile
    {
        public CreateCustomerMapper()
        {
            CreateMap<CreateCustomerRequest, Domain.Entities.Customer.Customer>();
        }
    }
}
