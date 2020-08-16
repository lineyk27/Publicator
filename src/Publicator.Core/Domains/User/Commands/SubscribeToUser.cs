using MediatR;
using Publicator.Core.Domains.User.Queries;

namespace Publicator.Core.Domains.User.Commands
{
    public class SubscribeToUser : LoggedInUser, IRequest<SubscriptionResult>
    {
        public string SubscriberUsername { get; set; }
    }
}
