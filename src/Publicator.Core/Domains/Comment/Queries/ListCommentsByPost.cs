using System;
using System.Collections.Generic;
using MediatR;

namespace Publicator.Core.Domains.Comment.Queries
{
    class ListCommentsByPost : IRequest<IEnumerable<Infrastructure.Models.Comment>>, IPageRequest
    {
        public Guid PostId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
