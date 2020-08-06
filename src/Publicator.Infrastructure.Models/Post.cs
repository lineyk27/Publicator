using System;
using System.Collections.Generic;

namespace Publicator.Infrastructure.Models
{
    public class Post : BaseEntity
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public int CurrentRating { get; set; }
        public Guid CreatorUserId { get; set; }
        public User CreatorUser { get; set; }
        public Guid? CommunityId { get; set; }
        public Community Community { get; set; }
        public ICollection<Vote> Votes{ get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<PostTag> PostTags { get; set; }
        public ICollection<SubscriptionNewPost> SubscriptionNewPosts { get; set; }
        public ICollection<Bookmark> Bookmarks{ get; set; }
    }
}
