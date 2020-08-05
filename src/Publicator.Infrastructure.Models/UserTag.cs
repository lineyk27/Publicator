using System;

namespace Publicator.Infrastructure.Entities
{
    public class UserTag : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
