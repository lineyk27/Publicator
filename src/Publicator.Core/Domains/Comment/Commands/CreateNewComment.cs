using System;
using MediatR;
using Publicator.Core.Domains.User.Queries;

namespace Publicator.Core.Domains.Comment.Commands
{
    public class CreateNewComment : LoggedInUser, IRequest<Infrastructure.Models.Comment>
    {
        public string Content { get; set; }
        public Guid PostId{ get; set; }
        public Guid? ParentRepliedCommentId { get; set; }
    }
}
