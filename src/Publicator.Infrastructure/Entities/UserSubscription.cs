using System;

namespace Publicator.Infrastructure.Entities
{
    public class UserSubscription : BaseEntity
    {
        public Guid SubscriberUserId { get; set; }
        public User SubscriberUser { get; set; }
        public Guid SubscriptionUserId { get; set; }
        public User SubscriptionUser { get; set; }
    }
}
