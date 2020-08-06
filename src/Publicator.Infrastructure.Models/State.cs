using System.Collections.Generic;

namespace Publicator.Infrastructure.Models
{
    public class State :  BaseEntity
    {
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
