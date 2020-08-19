using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Publicator.Core.DTO;
using Publicator.Infrastructure;

namespace Publicator.Core.Domains.Vote.Commands
{
    class VoteForPostHandler : IRequestHandler<VoteForPost, VoteDTO>
    {
        private readonly PublicatorDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<VoteForPostHandler> _logger;
        public VoteForPostHandler(PublicatorDbContext context, IMapper mapper, ILogger<VoteForPostHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
        
        public async Task<VoteDTO> Handle(
            VoteForPost request, 
            CancellationToken cancellationToken
            )
        {
            var currentVote = (from v in _context.Votes
                               where v.PostId == request.PostId
                               select v)
                               .FirstOrDefault();


            var post = _context.Posts.FirstOrDefault(x => x.Id == request.PostId);
            if (currentVote == null)
            {
                var newVote = new Infrastructure.Models.Vote()
                {
                    PostId = request.PostId,
                    UserId = (Guid)request.UserId,
                    Up = request.Up,
                    CreationDate = DateTime.Now
                };
                _context.Votes.Add(newVote);

                post.CurrentRating += request.Up ? 1 : -1;

                _logger.LogInformation("Added new vote of user {0} on post {1}", request.UserId, request.PostId);
            }
            else
            {
                if(currentVote.Up == request.Up)
                {

                    _logger.LogInformation("Removed a vote of user {0} on post {1}", request.UserId, request.PostId);
                    _context.Votes.Remove(currentVote);
                    post.CurrentRating += currentVote.Up ? -1 : 1;
                }
                else
                {
                    _logger.LogInformation("Added new vote of user {0} on post {1}", request.UserId, request.PostId);
                    currentVote.Up = request.Up;
                    _context.Votes.Update(currentVote);
                    post.CurrentRating += currentVote.Up ? 2 : -2;
                }
            }

            _context.Posts.Update(post);
            
            await _context.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<Infrastructure.Models.Vote, VoteDTO>(currentVote);

            return dto;
        }
    }
}
