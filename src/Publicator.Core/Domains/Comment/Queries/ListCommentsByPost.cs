using System;
using System.Collections.Generic;
using MediatR;
using Publicator.Core.DTO;

namespace Publicator.Core.Domains.Comment.Queries
{
    class ListCommentsByPost : IRequest<IEnumerable<CommentDTO>>, IPageRequest
    {
        public Guid PostId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
