using MediatR;
using Publicator.Core.DTO;
using System;

namespace Publicator.Core.Domains.Comment.Commands
{
    public class CreateNewComment : IRequest<CommentDTO>
    {
        public string Content { get; set; }
        public Guid PostId { get; set; }
        public Guid? ParentRepliedCommentId { get; set; }
    }
}
