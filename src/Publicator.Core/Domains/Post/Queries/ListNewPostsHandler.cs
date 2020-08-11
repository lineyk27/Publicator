using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Publicator.Core.Domains.Post.Queries;
using Publicator.Infrastructure;
using Publicator.Infrastructure.Models;

namespace Publicator.Core.Handlers
{
    class ListNewPostsHandler : IRequestHandler<ListNewPosts, IEnumerable<Post>>
    {
        private readonly PublicatorDbContext _context;

        public ListNewPostsHandler(PublicatorDbContext context) => _context = context;

        public async Task<IEnumerable<Post>> Handle(ListNewPosts request, CancellationToken cancellationToken)
        {
            var posts = (from p in _context.Posts
                         orderby p.CreationDate
                         select p
                         ).Skip(request.PageSize * (request.Page - 1))
                         .Take(request.PageSize);

            if (cancellationToken.IsCancellationRequested)
                return null;

            return await Task.Run(() => posts.ToList());
        }
    }
}
