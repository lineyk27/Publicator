using System.Collections.Generic;
using MediatR;
using Publicator.Core.Domains.User.Queries;
using Publicator.Core.DTO;

namespace Publicator.Core.Domains.Post.Queries
{
    public class ListPostsBySubscription : 
        IRequest<IEnumerable<PostDTO>>, 
        IPageRequest
    {
        public int PageSize { get; set; }
        public int Page { get; set; }
    }
}
