using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Publicator.Core.Domains.User.Queries
{
    class CurrentUserPipe<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : CurrentUserId
    {
        private readonly IHttpContextAccessor _httpContext;
        public CurrentUserPipe(IHttpContextAccessor httpContext) => _httpContext = httpContext;
        public async Task<TResponse> Handle(
            TRequest request, 
            CancellationToken cancellationToken, 
            RequestHandlerDelegate<TResponse> next
            )
        {
            var userId = _httpContext.HttpContext.User?.Claims
                .FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value;

            var id = Guid.Parse(userId);

            request.UserId = id;

            return await next();
            
        }
    }
}
