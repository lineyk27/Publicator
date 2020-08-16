using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Publicator.Core.DTO;
using Publicator.Infrastructure;

namespace Publicator.Core.Domains.User.Queries
{
    class GetByUsernameHandler : IRequestHandler<GetByUsername, UserDTO>
    {
        private readonly PublicatorDbContext _context;
        private readonly IMapper _mapper;
        public GetByUsernameHandler(PublicatorDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<UserDTO> Handle(GetByUsername request, CancellationToken cancellationToken)
        {
            var user = (from u in _context.Users
                        where u.Nickname.Equals(request.Username)
                        select u)
                        .FirstOrDefault();

            var dto = _mapper.Map<Infrastructure.Models.User, UserDTO>(user);

            return await Task.FromResult(dto);
        }
    }
}
