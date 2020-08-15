using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Publicator.Core.DTO;
using Publicator.Infrastructure;

namespace Publicator.Core.Domains.User.Queries
{
    class GetCurrentUserHandler : IRequestHandler<LoggedInUser, UserDTO>
    {
        private readonly PublicatorDbContext _context;
        private readonly IMapper _mapper;
        public GetCurrentUserHandler(PublicatorDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<UserDTO> Handle(
            LoggedInUser request,
            CancellationToken cancellationToken
            )
        {
            if(request.UserId != null)
            {
                var user = _context.Users.FirstOrDefault(x => x.Id == request.UserId);
                var dto = _mapper.Map<Infrastructure.Models.User, UserDTO>(user);

                return await Task.FromResult(dto);
            }

            return null;
        }
    }
}
