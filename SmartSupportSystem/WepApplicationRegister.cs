using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.settingOption;
using Shared.ErrorDto;
using SmartSupportSystem.WepApi.CustomMiddleWare;
using System.Text;

namespace SmartSupportSystem.WepApi
{
    public static class WepApplicationRegister
    {
        public static IServiceCollection AddWepApplicationRegister(this IServiceCollection services)
        {
            services.AddExceptionHandler<ExceptionHandel>();
            services.AddProblemDetails();
            services.Configure<ApiBehaviorOptions>((Option) =>
            {
                GenrateValidationErrors(Option);

            });

        

            return services;



        }
        
        public static IServiceCollection AddServiceJwt(this IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            var options = provider.GetRequiredService<IOptions<JwtSettingOption>>().Value;

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option => {

                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = options.Issuer,

                    ValidateAudience = true,
                    ValidAudience = options.Audience,

                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey)),
                };
                

            })
                ;

            return services;
        }

        private static void GenrateValidationErrors(ApiBehaviorOptions Option)
        {
            Option.InvalidModelStateResponseFactory = (context) =>
            {
                var errors = context.ModelState
                    .Where(e => e.Value!.Errors.Count > 0)
                    .Select(e => new ValidationError
                    {
                        Feild = e.Key,
                        Errors = e.Value!.Errors.Select(x => x.ErrorMessage).ToList()
                    }).ToList();
                var errorResponse = new ValidationErrorToReturn
                {
                    message = "Validation failed",
                    validationErrors = errors
                };
                return new BadRequestObjectResult(errorResponse);
            };
        }
    }
}
