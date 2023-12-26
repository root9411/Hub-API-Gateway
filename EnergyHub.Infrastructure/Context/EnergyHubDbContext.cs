using System.Data;
using Microsoft.EntityFrameworkCore;
using EnergyHub.Application.Context;
using EnergyHub.Domain.Entities.Customer;

namespace EnergyHub.Infrastructure.Context
{
    public class EnergyHubDbContext : DbContext, IEnergyHubDbContext
    {
        public EnergyHubDbContext(DbContextOptions<EnergyHubDbContext> options) : base(options)
        {

        }
        public bool HasChanges => ChangeTracker.HasChanges();
        public DbSet<Customer> Customers { get; set; }
        public async Task<int> SaveToDbAsync()
        {
            return await SaveChangesAsync();
        }
    }
}
