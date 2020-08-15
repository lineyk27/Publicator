using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Publicator.Core.DTO;
using Publicator.Infrastructure;

namespace Publicator.Core.Domains.Vote.Queries
{
    class GetCurrentVoteHandler : IRequestHandler<GetCurrentVote, VoteDTO>
    {
        private readonly PublicatorDbContext _context;
        private readonly IMapper _mapper;
        public GetCurrentVoteHandler(PublicatorDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<VoteDTO> Handle(
            GetCurrentVote request, 
            CancellationToken cancellationToken
            )
        {
            var vote = (from v in _context.Votes
                        where v.PostId == request.PostId && v.UserId == request.UserId
                        select v)
                        .SingleOrDefault();

            var dto = _mapper.Map<Infrastructure.Models.Vote, VoteDTO>(vote);

            return await Task.FromResult(dto);
        }
    }
}
