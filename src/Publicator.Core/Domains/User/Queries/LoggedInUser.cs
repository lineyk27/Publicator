using System;
using MediatR;
using Publicator.Core.DTO;

namespace Publicator.Core.Domains.User.Queries
{
    public class LoggedInUser : IRequest<UserDTO>
    {
        public Guid? UserId { get; set; }
    }
}
