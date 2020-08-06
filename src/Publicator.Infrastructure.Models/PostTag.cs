using System;

namespace Publicator.Infrastructure.Models
{
    public class PostTag : BaseEntity
    {
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        public Guid TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
