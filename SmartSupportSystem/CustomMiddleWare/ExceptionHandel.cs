using Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Shared.ErrorDto;

namespace SmartSupportSystem.WepApi.CustomMiddleWare
{
    public class ExceptionHandel(ILogger<ExceptionHandel> logger) : IExceptionHandler
    {
        private readonly ILogger<ExceptionHandel> _Logger = logger;

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _Logger.LogError(exception, "SomeThing Went Wrong.");

            var Resposne = new ErrorToReturn
            {

                ErrorMassage = exception.Message

            };

            httpContext.Response.StatusCode = exception switch
            {
                DuplicateEmailException => StatusCodes.Status409Conflict,
                NotFoundException => StatusCodes.Status404NotFound,
                BadRequestException badRequestException => GetBadRequest(Resposne, badRequestException),
                UnauthorizeException => StatusCodes.Status401Unauthorized,
                _ => StatusCodes.Status500InternalServerError
            };

            Resposne.status = httpContext.Response.StatusCode;

            await httpContext.Response.WriteAsJsonAsync(Resposne, cancellationToken);

            return true;

        }

        private static int GetBadRequest(ErrorToReturn Resposne, BadRequestException badRequestException)
        {
            Resposne.Errors = badRequestException.Errors;
            return StatusCodes.Status400BadRequest;

        }
    }
}
