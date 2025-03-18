using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Publicator.Presentation.Helpers;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Publicator.Presentation.Handlers
{
    public class ExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> _logger;

        public ExceptionHandler(ILogger<ExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var code = HttpStatusCode.BadRequest;

            _logger.LogError(exception, "Error message: {message}", exception.Message);

            var result = new Error() { Message = exception.Message };

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)code;
            await httpContext.Response.WriteAsync(result.ToString());

            return true;
        }
    }
}
