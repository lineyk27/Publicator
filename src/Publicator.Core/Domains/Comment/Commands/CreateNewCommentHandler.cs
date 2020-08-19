using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Publicator.Core.DTO;
using Publicator.Infrastructure;

namespace Publicator.Core.Domains.Comment.Commands
{
    class CreateNewCommentHandler :
        IRequestHandler<CreateNewComment, CommentDTO>
    {
        private readonly PublicatorDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateNewCommentHandler> _logger;
        public CreateNewCommentHandler(
            PublicatorDbContext context, 
            IMapper mapper, 
            ILogger<CreateNewCommentHandler> logger
            )
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
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

            _logger.LogInformation("Created new comment with id: {0}", newComment.Id);

            var dto = _mapper.Map<Infrastructure.Models.Comment, CommentDTO>(newComment);

            return dto;
        }
    }
}
