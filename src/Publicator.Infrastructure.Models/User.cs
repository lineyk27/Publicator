using System;
using System.Collections.Generic;

namespace Publicator.Infrastructure.Entities
{
    public class User : BaseEntity
    {
        public string Nickname { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime BeginStateDate { get; set; }
        public DateTime EndStateDate { get; set; }
        public string PictureName{ get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool EmailConfirmed { get; set; }
        public Guid StateId { get; set; }
        public State State { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public ICollection<UserSubscription> Subscriptions { get; set; }
        public ICollection<UserSubscription> Subscribers { get; set; }
        public ICollection<Vote> Votes{ get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Bookmark> Bookmarks{ get; set; }
        public ICollection<UserTag> SubscribeTags { get; set; }
        public ICollection<SubscriptionNewPost> SubscriptionNewPosts { get; set; }
        public ICollection<UserCommunity> UserCommunities { get; set; }
        public ICollection<Community> CreatedCommunities { get; set; }
    }
}
