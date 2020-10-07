using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<Infrastructure.Models.User> _userManager;
        public SubscribeToUserHandler(
            PublicatorDbContext context,
            ILogger<SubscribeToUserHandler> logger,
            UserManager<Infrastructure.Models.User> userManager
            )
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }
        public async Task<SubscriptionResult> Handle(
            SubscribeToUser request,
            CancellationToken cancellationToken
            )
        {
            var subscriptionUser = await _userManager.FindByNameAsync(request.Username);

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
