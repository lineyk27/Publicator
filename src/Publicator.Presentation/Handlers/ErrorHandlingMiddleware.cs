using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Publicator.ApplicationCore.Exceptions;
using Publicator.Presentation.Helpers;

namespace Publicator.Presentation.Handlers
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;

            if (ex is ResourceNotFoundException) code = HttpStatusCode.NotFound;
            else if (ex is AuthentificationException) code = HttpStatusCode.Unauthorized;
            else if (ex is ResourceException) code = HttpStatusCode.Conflict;
            else code = HttpStatusCode.BadRequest;

            var result = new Error() { Message = ex.Message };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result.ToString());
        }
    }
}
