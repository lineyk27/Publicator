using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Publicator.Infrastructure;

namespace Publicator.Core.Domains.Post.Commands
{
    class AddPostToBookmarksHandler : IRequestHandler<AddPostToBookmarks, BookmarkResult>
    {
        private readonly PublicatorDbContext _context;
        public AddPostToBookmarksHandler(PublicatorDbContext context) => _context = context;
        public async Task<BookmarkResult> Handle(
            AddPostToBookmarks request, 
            CancellationToken cancellationToken
            )
        {
            var foundBookmarks = await (from b in _context.Bookmarks
                                        where b.PostId.Equals(request.PostId) && 
                                              b.UserId.Equals(request.UserId)
                                        select b
                                        ).FirstOrDefaultAsync();
            var result = new BookmarkResult();
            if(foundBookmarks == null)
            {
                _context.Bookmarks.Add(new Infrastructure.Models.Bookmark()
                {
                    UserId = request.UserId,
                    PostId = request.PostId
                });
                await _context.SaveChangesAsync(cancellationToken);
                result.IsBookmarked = true;
            }
            else
            {
                _context.Bookmarks.Remove(foundBookmarks);
                result.IsBookmarked = false;
            }
            await _context.SaveChangesAsync(cancellationToken);
            
            return result;
        }
    }
}
