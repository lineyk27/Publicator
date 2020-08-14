using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Publicator.Infrastructure;

namespace Publicator.Core.Domains.User.Queries
{
    class GetUserByIdHandler : IRequestHandler<GetUserById, Infrastructure.Models.User>
    {
        private readonly PublicatorDbContext _context;
        public GetUserByIdHandler(PublicatorDbContext context) => _context = context;
        public async Task<Infrastructure.Models.User> Handle(
            GetUserById request, 
            CancellationToken cancellationToken
            )
        {
            var user = (from u in _context.Users
                        where u.Id == request.UserId
                        select u);

            return await Task.Run(() => user.FirstOrDefault());
        }
    }
}
