using Microsoft.AspNetCore.Mvc;
using Shared.ErrorDto;
using SmartSupportSystem.WepApi.CustomMiddleWare;

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

        private static void GenrateValidationErrors(ApiBehaviorOptions Option)
        {
            Option.InvalidModelStateResponseFactory = (context) =>
            {
                var errors = context.ModelState
                    .Where(e => e.Value!.Errors.Count > 0)
                    .Select(e => new ValidationError
                    {
                        Faild = e.Key,
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
