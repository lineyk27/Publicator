using MediatR;
using Publicator.Core.Domains.User.Commands;

namespace Publicator.Core.Domains.User.Queries
{
    public class GetCurrentSubscription : IRequest<SubscriptionResult>
    {
        public string Username { get; set; }
    }
}
