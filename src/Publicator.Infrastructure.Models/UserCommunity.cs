using System;

namespace Publicator.Infrastructure.Models
{
    public class UserCommunity : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid CommunityId { get; set; }
        public Community Community { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
