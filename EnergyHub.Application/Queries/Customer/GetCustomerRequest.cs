using System;
using MediatR;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace EnergyHub.Application.Queries.Customer
{
    public class GetCustomerRequest : IRequest<List<EnergyHub.Domain.Entities.Customer.Customer>>
    {
        public string Id { get; set; }
        public string EnvironmentDbName { get; set; }
    }
}
