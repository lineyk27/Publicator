using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Publicator.Core.DTO;
using Publicator.Infrastructure;

namespace Publicator.Core.Domains.Community.Queries
{
    class GetCommunityByIdHandler : IRequestHandler<GetCommunityById, CommunityDTO>
    {
        private readonly PublicatorDbContext _context;
        private readonly IMapper _mapper;
        public GetCommunityByIdHandler(PublicatorDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CommunityDTO> Handle(
            GetCommunityById request, 
            CancellationToken cancellationToken
            )
        {
            var community = await (from c in _context.Communities
                             where c.Id == request.CommunityId
                             select c
                             ).FirstOrDefaultAsync();

            var dto = _mapper.Map<Infrastructure.Models.Community, CommunityDTO>(community);

            return dto;
        }
    }
}
