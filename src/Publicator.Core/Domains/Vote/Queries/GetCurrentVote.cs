using System;
using MediatR;
using Publicator.Core.Domains.User.Queries;
using Publicator.Core.DTO;

namespace Publicator.Core.Domains.Vote.Queries
{
    public class GetCurrentVote : LoggedInUser, IRequest<VoteDTO>
    {
        public Guid PostId { get; set; }
    }
}
