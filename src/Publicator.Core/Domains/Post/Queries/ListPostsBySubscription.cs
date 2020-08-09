using System.Collections.Generic;
using MediatR;
using Publicator.Core.Domains.User.Queries;

namespace Publicator.Core.Domains.Post.Queries
{
    class ListPostsBySubscription : CurrentUserId, IRequest<IEnumerable<Infrastructure.Models.Post>>, IPageRequest
    {
        public int PageSize { get; set; }
        public int Page { get; set; }
    }
}
