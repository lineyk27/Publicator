using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Publicator.Infrastructure;

namespace Publicator.Core.Domains.Post.Queries
{
    class ListPostsBySubscriptionHandler :
        IRequestHandler<ListPostsBySubscription, IEnumerable<Infrastructure.Models.Post>>
    {
        private readonly PublicatorDbContext _context;
        public ListPostsBySubscriptionHandler(PublicatorDbContext context) => _context = context;
        public async Task<IEnumerable<Infrastructure.Models.Post>> Handle(
            ListPostsBySubscription request, 
            CancellationToken cancellationToken
            )
        {
            var posts = (from p in _context.Posts
                         join s in _context.SubscriptionNewPosts on p.Id equals s.PostId
                         where s.UserId == request.UserId
                         select p
                         );

            return await Task.Run(() => posts.ToList());
        }
    }
}
