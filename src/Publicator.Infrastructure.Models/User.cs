using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Publicator.Infrastructure.Models
{
    public class User : IdentityUser<Guid>
    {
        public DateTime JoinDate { get; set; }
        public string PictureUrl{ get; set; }
        public ICollection<UserSubscription> Subscriptions { get; set; }
        public ICollection<UserSubscription> Subscribers { get; set; }
        public ICollection<Vote> Votes{ get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Bookmark> Bookmarks{ get; set; }
        public ICollection<UserTag> SubscribeTags { get; set; }
        public ICollection<SubscriptionNewPost> SubscriptionNewPosts { get; set; }
        public ICollection<SubscriptionNewPost> SubscribersNewPosts { get; set; }
        public ICollection<UserCommunity> UserCommunities { get; set; }
        public ICollection<Community> CreatedCommunities { get; set; }
    }
}
