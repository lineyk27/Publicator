using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Publicator.Core.DTO;
using Publicator.Infrastructure;

namespace Publicator.Core.Domains.Comment.Queries
{
    class ListCommentsByPostHandler :
        IRequestHandler<ListCommentsByPost, IEnumerable<CommentDTO>>
    {
        private readonly PublicatorDbContext _context;
        private readonly IMapper _mapper;
        public ListCommentsByPostHandler(PublicatorDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<CommentDTO>> Handle(
            ListCommentsByPost request, 
            CancellationToken cancellationToken
            )
        {
            // up to 6 levels of comments
            var comments = (from c in _context
                            .Comments
                            .Include("RepliesComments.User")
                            .Include("RepliesComments.RepliesComments.User")
                            .Include("RepliesComments.RepliesComments.RepliesComments.User")
                            .Include("RepliesComments.RepliesComments.RepliesComments.RepliesComments.User")
                            .Include("RepliesComments.RepliesComments.RepliesComments.RepliesComments" +
                            ".RepliesComments.User")
                            where c.ParentRepliedCommentId == null && c.PostId == request.PostId
                            select c);
            
            var dtos = _mapper.Map<
                IEnumerable<Infrastructure.Models.Comment>,
                IEnumerable<CommentDTO>>(await comments.ToListAsync());

            return dtos;
        }
    }
}
