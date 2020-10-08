using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Publicator.Core.Services;
using Publicator.Infrastructure;

namespace Publicator.Core.Domains.Post.Commands
{
    class AddPostToBookmarksHandler : IRequestHandler<AddPostToBookmarks, BookmarkResult>
    {
        private readonly PublicatorDbContext _context;
        private readonly ILogger<AddPostToBookmarks> _logger;
        private readonly IAuthService _authService;
        public AddPostToBookmarksHandler(
            PublicatorDbContext context,
            ILogger<AddPostToBookmarks> logger,
            IAuthService authService
            )
        { 
            _context = context;
            _logger = logger;
            _authService = authService;
        }
        public async Task<BookmarkResult> Handle(
            AddPostToBookmarks request, 
            CancellationToken cancellationToken
            )
        {
            var userId = _authService.GetCurrentUserId();

            var foundBookmarks = await (from b in _context.Bookmarks
                                        where b.PostId.Equals(request.PostId) &&
                                              b.UserId.Equals(userId)
                                        select b
                                        ).FirstOrDefaultAsync();
            var result = new BookmarkResult();
            if(foundBookmarks == null)
            {
                _context.Bookmarks.Add(new Infrastructure.Models.Bookmark()
                {
                    UserId = (Guid)userId,
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

            if (result.IsBookmarked)
                _logger.LogInformation("Added post to bookmarks with id: {}", request.PostId);
            else
                _logger.LogInformation("Removed post from bookmarks with id: {}", request.PostId);

            return result;
        }
    }
}
