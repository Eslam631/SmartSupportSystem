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

            httpContext.Response.StatusCode = exception switch
            {
              
                _ => StatusCodes.Status500InternalServerError
            };

            var Error= new ErrorToReturn
            {
                status = httpContext.Response.StatusCode,
                Error = exception.Message
            };
          await  httpContext.Response.WriteAsJsonAsync(Error, cancellationToken);

            return true;

        }
    }
}
