using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Publicator.Core.DTO;
using Publicator.Infrastructure;

namespace Publicator.Core.Domains.Comment.Commands
{
    class CreateNewCommentHandler :
        IRequestHandler<CreateNewComment, CommentDTO>
    {
        private readonly PublicatorDbContext _context;
        private readonly IMapper _mapper;
        public CreateNewCommentHandler(PublicatorDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<CommentDTO> Handle(
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

            var dto = _mapper.Map<Infrastructure.Models.Comment, CommentDTO>(newComment);

            return dto;
        }
    }
}
