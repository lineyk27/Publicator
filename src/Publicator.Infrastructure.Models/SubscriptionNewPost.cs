using System;

namespace Publicator.Infrastructure.Models
{
    public class SubscriptionNewPost : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        public Guid SubscriptionUserId { get; set; }
        public User SubscriptionUser { get; set; }
        public Guid SubscriptionTagId { get; set; }
        public Tag SubscriptionTag { get; set; }
        public Guid SubscriptionCommunityId { get; set; }
        public Community SubscriptionCommunity { get; set; }
    }
}
