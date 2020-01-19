using System;
using System.Collections.Generic;

namespace Publicator.Infrastructure.Entities
{
    public class Community : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public string PictureName { get; set; }
        public Guid CreatorUserId { get; set; }
        public User CreatorUser { get; set; }
        public ICollection<UserCommunity> UserCommunities { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<SubscriptionNewPost> SubscriptionNewPosts { get; set; }
    }
}
