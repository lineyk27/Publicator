using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Publicator.Core.Domains.User.Commands;
using Publicator.Core.Services;
using Publicator.Infrastructure;

namespace Publicator.Core.Domains.User.Queries
{
    class GetCurrentSubscriptionHandler : IRequestHandler<GetCurrentSubscription, SubscriptionResult>
    {
        private readonly PublicatorDbContext _context;
        private readonly IAuthService _authService;
        public GetCurrentSubscriptionHandler(
            PublicatorDbContext context,
            IAuthService authService
            )
        {
            _context = context;
            _authService = authService;
        }
        public async Task<SubscriptionResult> Handle(
            GetCurrentSubscription request, 
            CancellationToken cancellationToken
            )
        {
            var userId = _authService.GetCurrentUserId();

            var subscriptionUser = await _context.Users
                .FirstOrDefaultAsync(x => x.UserName.Equals(request.Username));

            var currentSubscription = await(from s in _context.UserSubscriptions.Include(x => x.SubscriptionUser)
                                            where s.SubscriberUserId == userId &&
                                                    s.SubscriptionUserId == subscriptionUser.Id
                                            select s
                                            ).FirstOrDefaultAsync(cancellationToken);

            var result = new SubscriptionResult();

            result.IsSubscribed = currentSubscription != null;
            
            return result;
        }
    }
}
