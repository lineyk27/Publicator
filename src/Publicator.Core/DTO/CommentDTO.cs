using System;
using System.Collections.Generic;

namespace Publicator.Core.DTO
{
    public class CommentDTO
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreationTime { get; set; }
        public UserDTO CreatorUser { get; set; }
        public IEnumerable<CommentDTO> Replies { get; set; }
    }
}
