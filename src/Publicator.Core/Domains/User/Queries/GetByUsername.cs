using MediatR;
using Publicator.Core.DTO;

namespace Publicator.Core.Domains.User.Queries
{
    public class GetByUsername : IRequest<UserDTO>
    {
        public string Username { get; set; }
    }
}
