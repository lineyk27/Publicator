using MediatR;
using Publicator.Core.Domains.User.Queries;
using Publicator.Core.DTO;
using System;

namespace Publicator.Core.Domains.Vote.Commands
{
    public class VoteForPost : LoggedInUser, IRequest<VoteDTO>
    {
        public Guid PostId { get; set; }
        public bool Up { get; set; }
    }
}
