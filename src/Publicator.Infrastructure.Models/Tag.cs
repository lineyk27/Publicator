using System.Collections.Generic;

namespace Publicator.Infrastructure.Models
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<PostTag> PostTags { get; set; }
        public ICollection<UserTag> UserTags { get; set; }
    }
}
