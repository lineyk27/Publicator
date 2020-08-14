using System;
using System.Collections.Generic;
using MediatR;

namespace Publicator.Core.Domains.User.Queries
{
    public class GetUserById : IRequest<Infrastructure.Models.User>
    {
        public Guid UserId { get; set; }
    }
}
