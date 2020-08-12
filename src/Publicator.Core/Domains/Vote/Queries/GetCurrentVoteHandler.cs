using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Publicator.Core.Domains.Vote.Queries
{
    class GetCurrentVoteHandler : IRequestHandler<GetCurrentVote, Infrastructure.Models.Vote>
    {
        private readonly Infrastructure.PublicatorDbContext _context;
        public GetCurrentVoteHandler(Infrastructure.PublicatorDbContext context) => _context = context;
        public async Task<Infrastructure.Models.Vote> Handle(
            GetCurrentVote request, 
            CancellationToken cancellationToken
            )
        {
            var vote = (from v in _context.Votes
                        where v.PostId == request.PostId && v.UserId == request.UserId
                        select v)
                        .SingleOrDefault();

            return await Task.FromResult(vote);
        }
    }
}
