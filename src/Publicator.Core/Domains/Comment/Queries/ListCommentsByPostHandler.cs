using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Publicator.Infrastructure;

namespace Publicator.Core.Domains.Comment.Queries
{
    class ListCommentsByPostHandler : 
        IRequestHandler<ListCommentsByPost, IEnumerable<Infrastructure.Models.Comment>>
    {
        private readonly PublicatorDbContext _context;
        public ListCommentsByPostHandler(PublicatorDbContext context) => _context = context;
        public async Task<IEnumerable<Infrastructure.Models.Comment>> Handle(
            ListCommentsByPost request, 
            CancellationToken cancellationToken
            )
        {
            // up to 6 levels of comments
            var comments = (from c in _context
                            .Comments
                            .Include(x => x.RepliesComments)
                            .ThenInclude(x => x.Select(x => x.RepliesComments))
                            .ThenInclude(x => x.Select(x => x.RepliesComments))
                            .ThenInclude(x => x.Select(x => x.RepliesComments))
                            .ThenInclude(x => x.Select(x => x.RepliesComments))
                            
                            join p in (from p1 in _context.Posts 
                                       where p1.Id == request.PostId
                                       select p1) on c.PostId equals p.Id
                            
                            where c.ParentRepliedCommentId == null
                            select c);

            comments = comments
                .Skip(request.PageSize * (request.Page - 1))
                .Take(request.PageSize);

            return await Task.Run(() => comments.ToList());
        }
    }
}
