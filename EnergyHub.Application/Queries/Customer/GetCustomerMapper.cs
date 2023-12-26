using AutoMapper;

namespace EnergyHub.Application.Queries.Customer
{
    public class GetCustomerMapper : Profile
    {
        public GetCustomerMapper() 
        {
            CreateMap<EnergyHub.Domain.Entities.Customer.Customer, GetCustomerRequest>();
        }
    }
}
