using System;
using MediatR;
using Publicator.Core.Domains.User.Queries;
using Publicator.Core.DTO;

namespace Publicator.Core.Domains.Comment.Commands
{
    public class CreateNewComment : IRequest<CommentDTO>
    {
        public string Content { get; set; }
        public Guid PostId{ get; set; }
        public Guid? ParentRepliedCommentId { get; set; }
    }
}
