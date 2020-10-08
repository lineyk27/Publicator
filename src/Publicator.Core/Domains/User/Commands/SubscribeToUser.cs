using MediatR;
using Publicator.Core.Domains.User.Queries;

namespace Publicator.Core.Domains.User.Commands
{
    public class SubscribeToUser : IRequest<SubscriptionResult>
    {
        public string Username { get; set; }
    }
}
