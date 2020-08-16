using System;
using System.Collections.Generic;
using MediatR;
using Publicator.Core.DTO;

namespace Publicator.Core.Domains.Post.Queries
{
    public class ListPostsByCommunity : IRequest<IEnumerable<PostDTO>>, IPageRequest
    {
        public Guid CommunityId { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
    }
}
