using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Publicator.Core.DTO;
using Publicator.Infrastructure;

namespace Publicator.Core.Domains.Community.Queries
{
    class ListAllCommunitiesHandler : IRequestHandler<ListAllCommunities, IEnumerable<CommunityDTO>>
    {
        private readonly PublicatorDbContext _context;
        private readonly IMapper _mapper;
        public ListAllCommunitiesHandler(PublicatorDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CommunityDTO>> Handle(
            ListAllCommunities request, 
            CancellationToken cancellationToken
            )
        {
            var communities = await _context.Communities.ToListAsync();

            var dtos = _mapper
                .Map<IEnumerable<Infrastructure.Models.Community>, IEnumerable<CommunityDTO>>(communities);

            return dtos;
        }
    }
}
