using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Publicator.Core.Domains.User.Commands;
using Publicator.Infrastructure;

namespace Publicator.Core.Domains.User.Queries
{
    class GetCurrentSubscriptionHandler : IRequestHandler<GetCurrentSubscription, SubscriptionResult>
    {
        private readonly PublicatorDbContext _context;
        public GetCurrentSubscriptionHandler(PublicatorDbContext context) => _context = context; 
        public async Task<SubscriptionResult> Handle(
            GetCurrentSubscription request, 
            CancellationToken cancellationToken
            )
        {
            var subscriptionUser = await _context.Users
                .FirstOrDefaultAsync(x => x.Nickname.Equals(request.SubscriberUsername));

            var currentSubscription = await(from s in _context.UserSubscriptions.Include(x => x.SubscriptionUser)
                                            where s.SubscriberUserId.Equals(request.UserId) &&
                                                    s.SubscriptionUser.Nickname.Equals(subscriptionUser.Id)
                                            select s
                                            ).FirstOrDefaultAsync(cancellationToken);

            var result = new SubscriptionResult();

            result.IsSubscribed = currentSubscription != null;
            
            return result;
        }
    }
}
