using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using MediatR;
using Publicator.Infrastructure.Models;
using Publicator.Infrastructure;
using Publicator.Core.Domains.Post.Queries;

namespace Publicator.Core.Handlers
{
    class ListHotPostsHandler : IRequestHandler<ListHotPosts, IEnumerable<Post>>
    {
        private readonly PublicatorDbContext _context;

        public ListHotPostsHandler(PublicatorDbContext context) => _context = context;

        public async Task<IEnumerable<Post>> Handle(ListHotPosts request, CancellationToken cancellationToken)
        {
            var startDate = DateTime.Now.AddDays(-(int)request.Period);
            
            var posts = (from p in _context.Posts
                         where p.CreationDate >= startDate
                         orderby p.CurrentRating
                         select p)
                         .Skip(request.PageSize * (request.Page-1))
                         .Take(request.PageSize);
            
            return await Task.Run((() => posts.ToList()));
        }
    }
}
