using Domain.Contracts;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data.Context;
using Persistence.Data.SeedData;
using Persistence.Repository;


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
            services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseSqlServer(Config.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentityCore<ApplicationUser>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

              
             
                options.User.RequireUniqueEmail = false;
            })
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<AuthorizationDbContext>();

            services.AddScoped<IDataSeed, DataSeed>();
         
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            services.AddOptions<SettingJsonAdmin>()
        .BindConfiguration(SettingJsonAdmin.SectionName);

            return services;
        }
    }
}
