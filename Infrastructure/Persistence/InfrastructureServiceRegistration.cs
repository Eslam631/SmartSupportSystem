using Domain.Contracts;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data.Context;
using Persistence.Data.SeedData;


namespace Persistence
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfraStructureServices(this IServiceCollection services, IConfiguration Config)
        {
            services.AddDbContext<AuthorizationDbContext>(option =>
            {
                option.UseSqlServer(Config.GetConnectionString("IdentityConnection"));
            });

            services.AddIdentityCore<ApplicationUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<AuthorizationDbContext>();

            services.AddScoped<IDataSeed, DataSeed>();




            return services;
        }
    }
}
