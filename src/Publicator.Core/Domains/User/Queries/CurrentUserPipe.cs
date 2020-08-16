using System;
using System.Security.Claims;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Publicator.Core.Domains.User.Queries
{
    class CurrentUserPipe<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IHttpContextAccessor _httpContext;
        public CurrentUserPipe(IHttpContextAccessor httpContext) => _httpContext = httpContext;
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

            return await next();
            
        }
    }
}
