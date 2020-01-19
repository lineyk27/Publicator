using System;
using System.Collections.Generic;

namespace Publicator.Infrastructure.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        public Guid? ParentRepliedCommentId { get; set; }
        public Comment ParentRepliedComment { get; set; }
        public ICollection<Comment> RepliesComments { get; set; }
    }
}
