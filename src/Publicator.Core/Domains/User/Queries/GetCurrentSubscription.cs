﻿using MediatR;
using Publicator.Core.Domains.User.Commands;

namespace Publicator.Core.Domains.User.Queries
{
    public class GetCurrentSubscription : LoggedInUser, IRequest<SubscriptionResult>
    {
        public string SubscriberUsername { get; set; }
    }
}
