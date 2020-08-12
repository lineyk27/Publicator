using MediatR;
using Publicator.Core.Domains.User.Queries;
using System;

namespace Publicator.Core.Domains.Vote.Commands
{
    class VoteForPost : CurrentUserId, IRequest<Infrastructure.Models.Vote>
    {
        public Guid PostId { get; set; }
        public bool Up { get; set; }
    }
}
