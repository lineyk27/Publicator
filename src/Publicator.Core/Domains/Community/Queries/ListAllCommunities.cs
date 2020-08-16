using System;
using System.Collections.Generic;
using MediatR;
using Publicator.Core.DTO;

namespace Publicator.Core.Domains.Community.Queries
{
    public class ListAllCommunities : IRequest<IEnumerable<CommunityDTO>>
    {
    }
}
