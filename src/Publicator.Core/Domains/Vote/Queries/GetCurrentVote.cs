using System;
using MediatR;
using Publicator.Core.Domains.User.Queries;

namespace Publicator.Core.Domains.Vote.Queries
{
    public class GetCurrentVote : LoggedInUser, IRequest<Infrastructure.Models.Vote>
    {
        public Guid PostId { get; set; }
    }
}
