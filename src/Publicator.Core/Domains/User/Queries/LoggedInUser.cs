using System;
using MediatR;

namespace Publicator.Core.Domains.User.Queries
{
    public class LoggedInUser : IRequest<Infrastructure.Models.User>
    {
        public Guid? UserId { get; set; }
    }
}
