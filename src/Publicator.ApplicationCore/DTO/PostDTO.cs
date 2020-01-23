using System;

namespace Publicator.ApplicationCore.DTO
{
    public class PostDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime CreationTime{ get;set; }
    }
}
