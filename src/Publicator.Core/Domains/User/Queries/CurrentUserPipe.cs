using System;
using System.Security.Claims;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Publicator.Core.Domains.User.Queries
{
    class CurrentUserPipe<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly ILogger<CurrentUserPipe<TRequest, TResponse>> _logger;
        public CurrentUserPipe(
            IHttpContextAccessor httpContext,
            ILogger<CurrentUserPipe<TRequest, TResponse>> logger
            )
        {
            _httpContext = httpContext;
            _logger = logger;
        }
        public async Task<TResponse> Handle(
            TRequest request, 
            CancellationToken cancellationToken, 
            RequestHandlerDelegate<TResponse> next
            )
        {
            if (!(request is LoggedInUser user))
                return await next();

                var userId = _httpContext.HttpContext?.User?.Claims?
                .FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value;

            if(userId != null)
            {
                Guid id = Guid.Parse(userId);
                (request as LoggedInUser).UserId = id;
            }
            else
            {
                _logger.LogTrace("The user was not authenticated");
            }

            return await next();
            
        }
    }
}
