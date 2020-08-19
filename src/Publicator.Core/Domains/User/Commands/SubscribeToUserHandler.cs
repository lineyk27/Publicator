using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Publicator.Infrastructure;
using Publicator.Infrastructure.Models;

namespace Publicator.Core.Domains.User.Commands
{
    class SubscribeToUserHandler : IRequestHandler<SubscribeToUser, SubscriptionResult>
    {
        private readonly PublicatorDbContext _context;
        private readonly ILogger<SubscribeToUserHandler> _logger;
        public SubscribeToUserHandler(
            PublicatorDbContext context,
            ILogger<SubscribeToUserHandler> logger
            )
        {
            _logger = logger;
            _context = context;
        }
        public async Task<SubscriptionResult> Handle(
            SubscribeToUser request,
            CancellationToken cancellationToken
            )
        {
            var subscriptionUser = await _context.Users
                .FirstOrDefaultAsync(x => x.Nickname.Equals(request.SubscriberUsername));

            var currentSubscription = await (from s in _context.UserSubscriptions.Include(x => x.SubscriptionUser)
                                            where s.SubscriberUserId.Equals(request.UserId) &&
                                                    s.SubscriptionUserId.Equals(subscriptionUser.Id)
                                            select s
                                            ).FirstOrDefaultAsync();

            var result = new SubscriptionResult();

            if(currentSubscription == null)
            {
                _context.UserSubscriptions.Add(new UserSubscription()
                {
                    SubscriberUserId = (Guid)request.UserId,
                    SubscriptionUserId = subscriptionUser.Id,
                });
                result.IsSubscribed = true;
                _logger.LogInformation(
                    "Added subscription of user with id {0} on user with id {1}",
                    request.UserId,
                    subscriptionUser.Id);
            }
            else
            {

                _context.UserSubscriptions.Remove(currentSubscription);
                result.IsSubscribed = false;
                _logger.LogInformation(
                    "Removed subscription of user with id {0} on user with id {1}",
                    request.UserId,
                    subscriptionUser.Id);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
