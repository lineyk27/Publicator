using System;
using MediatR;
using Publicator.Core.Domains.User.Queries;

namespace Publicator.Core.Domains.Vote.Queries
{
    class GetCurrentVote : CurrentUserId, IRequest<Infrastructure.Models.Vote>
    {
        public Guid PostId { get; set; }
    }
}
