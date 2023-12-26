using Microsoft.EntityFrameworkCore;
using EnergyHub.Domain.Entities.Customer;

namespace EnergyHub.Application.Context
{
    public interface IEnergyHubDbContext
    {
        DbSet<Customer> Customers { get; }
        Task<int> SaveToDbAsync();
    }
}
