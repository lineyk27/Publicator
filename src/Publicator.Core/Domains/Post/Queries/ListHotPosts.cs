using MediatR;
using System.Collections.Generic;

namespace Publicator.Core.Domains.Post.Queries
{
    public class ListHotPosts : IRequest<IEnumerable<Infrastructure.Models.Post>>, IPageRequest
    {        
        public int PageSize { get ; set; }
        public int Page { get ; set ; }
        public HotPeriod Period { get; set; }
        public ListHotPosts(HotPeriod period, int page, int pageSize)
        {
            Period = period;
            Page = page;
            PageSize = pageSize;
        }
    }
}
