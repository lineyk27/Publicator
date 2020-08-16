using System;
using MediatR;
using Publicator.Core.DTO;

namespace Publicator.Core.Domains.Community.Queries
{
    public class GetCommunityById : IRequest<CommunityDTO>
    {
        public Guid CommunityId { get; set; }
    }
}
