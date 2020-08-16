using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Publicator.Infrastructure;
using Publicator.Core.DTO;
using AutoMapper;

namespace Publicator.Core.Domains.Post.Queries
{
    class ListPostsByCommunityHandler : IRequestHandler<ListPostsByCommunity, IEnumerable<PostDTO>>
    {
        private readonly PublicatorDbContext _context;
        private readonly IMapper _mapper;
        public ListPostsByCommunityHandler(PublicatorDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PostDTO>> Handle(
            ListPostsByCommunity request, 
            CancellationToken cancellationToken
            )
        {
            var posts = await (from p in _context.Posts
                         where p.CommunityId == request.CommunityId
                         select p
                )
                .Skip((request.Page-1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            
            var dtos = _mapper.Map<IEnumerable<Infrastructure.Models.Post>, IEnumerable<PostDTO>>(posts);
            
            return dtos;
        }
    }
}
