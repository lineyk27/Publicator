using MediatR;
using System.Collections.Generic;

namespace Publicator.Core.Domains.Post.Queries
{
    public class ListNewPosts : IRequest<IEnumerable<Infrastructure.Models.Post>>, IPageRequest
    {
        public int PageSize { get ; set; }
        public int Page { get ; set; }
        public ListNewPosts(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
    }
}
