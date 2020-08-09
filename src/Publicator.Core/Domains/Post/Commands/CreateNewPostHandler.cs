using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Publicator.Infrastructure;

namespace Publicator.Core.Domains.Post.Commands
{
    class CreateNewPostHandler : IRequestHandler<CreateNewPost, Infrastructure.Models.Post>
    {
        private readonly PublicatorDbContext _context;
        public CreateNewPostHandler(PublicatorDbContext context) => _context = context;
        public async Task<Infrastructure.Models.Post> Handle(
            CreateNewPost request, 
            CancellationToken cancellationToken)
        {
            var post = new Infrastructure.Models.Post()
            {
                Name = request.Name,
                Content = request.Content,
                CommunityId = request.CommunityId,
                CreationDate = DateTime.Now,
                CreatorUserId = request.UserId
            };

            _context.Posts.Add(post);

            if (cancellationToken.IsCancellationRequested)
                return null;

            await _context.SaveChangesAsync(cancellationToken);

            return post;
        }
    }
}
