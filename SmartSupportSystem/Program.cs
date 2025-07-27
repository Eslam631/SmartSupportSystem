
using Domain.Contracts;
using Persistence;
using Services;
using SmartSupportSystem.WepApi;

namespace SmartSupportSystem
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


           
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddInfraStructureServices(builder.Configuration);
            builder.Services.AddApplicationServices(builder.Configuration);
         
            builder.Services.AddServiceJwt();

            builder.Services.AddWepApplicationRegister();

            builder.Services.AddOpenApi();
           

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
              var seed=  scope.ServiceProvider.GetRequiredService<IDataSeed>();
               await seed.SeedAdminAsync();

            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(option => option.SwaggerEndpoint("/openapi/v1.json", "v1"));
            }

            app.UseExceptionHandler();
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
