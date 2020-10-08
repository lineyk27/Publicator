using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Publicator.Core.DTO;
using Publicator.Core.Services;
using Publicator.Infrastructure;

namespace Publicator.Core.Domains.Vote.Queries
{
    class GetCurrentVoteHandler : IRequestHandler<GetCurrentVote, VoteDTO>
    {
        private readonly PublicatorDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        public GetCurrentVoteHandler(
            PublicatorDbContext context, 
            IMapper mapper,
            IAuthService authService)
        {
            _context = context;
            _mapper = mapper;
            _authService = authService;
        }
        
        public async Task<VoteDTO> Handle(
            GetCurrentVote request, 
            CancellationToken cancellationToken
            )
        {
            var userId = _authService.GetCurrentUserId();
            var vote = (from v in _context.Votes
                        where v.PostId == request.PostId && v.UserId == userId
                        select v)
                        .SingleOrDefault();

            var dto = _mapper.Map<Infrastructure.Models.Vote, VoteDTO>(vote);

            return await Task.FromResult(dto);
        }
    }
}
