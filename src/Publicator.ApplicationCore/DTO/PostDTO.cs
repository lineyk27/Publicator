using System;
using System.Collections.Generic;

namespace Publicator.ApplicationCore.DTO
{
    public class PostDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate{ get;set; }
        public IEnumerable<TagDTO> Tags { get; set; }
    }
}
