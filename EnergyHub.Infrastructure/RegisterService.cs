using Microsoft.EntityFrameworkCore;
using EnergyHub.Application.Context;
using EnergyHub.Infrastructure.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EnergyHub.Domain.Repositories;
using EnergyHub.Application.Services.Customer;
using EnergyHub.Application.Services.Auth;
using EnergyHub.Infrastructure.Repositories.Customer;
using EnergyHub.Infrastructure.Services.Auth;
using EnergyHub.Infrastructure.Services.Customer;
using EnergyHub.Application.Services.Shared;
using EnergyHub.Infrastructure.Services.Shared;

namespace EnergyHub.Infrastructure
{
    public static class RegisterService
    {
        public static void ConfigureInfraStructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EnergyHubDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IEnergyHubDbContext>(option => {
                return option.GetService<EnergyHubDbContext>();
            });

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ISharedService, SharedService>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
        }
    }
}
