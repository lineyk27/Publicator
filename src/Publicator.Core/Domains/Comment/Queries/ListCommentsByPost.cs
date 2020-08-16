using System;
using System.Collections.Generic;
using MediatR;
using Publicator.Core.DTO;

namespace Publicator.Core.Domains.Comment.Queries
{
    public class ListCommentsByPost : IRequest<IEnumerable<CommentDTO>>
    {
        public Guid PostId { get; set; }
    }
}
