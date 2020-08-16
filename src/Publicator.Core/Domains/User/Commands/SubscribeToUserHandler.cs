using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Publicator.Infrastructure;
using Publicator.Infrastructure.Models;

namespace Publicator.Core.Domains.User.Commands
{
    class SubscribeToUserHandler : IRequestHandler<SubscribeToUser, SubscriptionResult>
    {
        private readonly PublicatorDbContext _context;
        public SubscribeToUserHandler(PublicatorDbContext context) => _context = context;
        public async Task<SubscriptionResult> Handle(
            SubscribeToUser request,
            CancellationToken cancellationToken
            )
        {
            var subscriptionUser = await _context.Users
                .FirstOrDefaultAsync(x => x.Nickname.Equals(request.SubscriberUsername));

            var currentSubscription = await (from s in _context.UserSubscriptions.Include(x => x.SubscriptionUser)
                                            where s.SubscriberUserId.Equals(request.UserId) &&
                                                    s.SubscriptionUser.Nickname.Equals(subscriptionUser.Id)
                                            select s
                                            ).FirstOrDefaultAsync();

            var result = new SubscriptionResult();

            if(currentSubscription != null)
            {
                _context.UserSubscriptions.Add(new UserSubscription()
                {
                    SubscriberUserId = (Guid)request.UserId,
                    SubscriptionUserId = subscriptionUser.Id,
                });
                result.IsSubscribed = true;
            }
            else
            {
                _context.UserSubscriptions.Remove(currentSubscription);
                result.IsSubscribed = false;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
