using MediatR;
using Publicator.Core.DTO;
using System.Collections.Generic;

namespace Publicator.Core.Domains.Post.Queries
{
    public class ListHotPosts : IRequest<IEnumerable<PostDTO>>, IPageRequest
    {        
        public int PageSize { get ; set; }
        public int Page { get ; set ; }
        public HotPeriod Period { get; set; }
    }
}
