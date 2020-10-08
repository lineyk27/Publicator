using MediatR;
using Publicator.Core.DTO;
using System;

namespace Publicator.Core.Domains.Vote.Commands
{
    public class VoteForPost : IRequest<VoteDTO>
    {
        public Guid PostId { get; set; }
        public bool Up { get; set; }
    }
}
