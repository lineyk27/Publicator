using System.Collections.Generic;
using MediatR;
using Publicator.Core.DTO;

namespace Publicator.Core.Domains.Post.Queries
{
    public class ListPostsByCreatorUser : IPageRequest, IRequest<IEnumerable<PostDTO>>
    {
        public string Username{ get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
    }
}
