using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Publicator.Infrastructure;

namespace Publicator.Core.Domains.User.Queries
{
    class GetCurrentUserHandler : IRequestHandler<LoggedInUser, Infrastructure.Models.User>
    {
        private readonly PublicatorDbContext _context;
        public GetCurrentUserHandler(PublicatorDbContext context) => _context = context;
        public async Task<Infrastructure.Models.User> Handle(
            LoggedInUser request,
            CancellationToken cancellationToken
            )
        {
            if(request.UserId != null)
            {
                var user = _context.Users.FirstOrDefault(x => x.Id == request.UserId);
                return await Task.FromResult(user);
            }

            return null;
        }
    }
}
