using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Publicator.Infrastructure;

namespace Publicator.Core.Domains.Comment.Commands
{
    class CreateNewCommentHandler : IRequestHandler<CreateNewComment, Infrastructure.Models.Comment>
    {
        private readonly PublicatorDbContext _context;
        public CreateNewCommentHandler(PublicatorDbContext context) => _context = context;
        public async Task<Infrastructure.Models.Comment> Handle(
            CreateNewComment request, 
            CancellationToken cancellationToken
            )
        {
            var newComment = new Infrastructure.Models.Comment()
            {
                Content = request.Content,
                ParentRepliedCommentId = request.ParentRepliedCommentId,
                PostId = request.PostId,
                UserId = (Guid)request.UserId,
                CreationDate = DateTime.Now
            };

            _context.Comments.Add(newComment);

            await _context.SaveChangesAsync(cancellationToken);

            return newComment;
        }
    }
}
