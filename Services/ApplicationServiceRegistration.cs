using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using Services.settingOption;

namespace Services
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<Func<IAuthService>>(serviceProvider =>
            () => serviceProvider.GetRequiredService<IAuthService>());

            services.AddOptions<JwtSettingOption>()
       .BindConfiguration(JwtSettingOption.SectionName);

            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<Func<IDepartmentService>>(serviceProvider => 
            () => serviceProvider.GetRequiredService<IDepartmentService>());
            return services;
        }
    }
}
