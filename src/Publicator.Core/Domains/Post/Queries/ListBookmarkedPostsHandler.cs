using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Publicator.Core.DTO;
using Publicator.Infrastructure;

namespace Publicator.Core.Domains.Post.Queries
{
    class ListBookmarkedPostsHandler : IRequestHandler<ListBookmarkedPosts, IEnumerable<PostDTO>>
    {
        private readonly PublicatorDbContext _context;
        private readonly IMapper _mapper;
        public ListBookmarkedPostsHandler(PublicatorDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PostDTO>> Handle(
            ListBookmarkedPosts request, 
            CancellationToken cancellationToken
            )
        {
            var posts = await (from b in _context.Bookmarks.Include(x => x.Post)
                                where b.UserId.Equals(request.UserId)
                                select b.Post
                                ).ToListAsync();

            var dtos = _mapper.Map<IEnumerable<Infrastructure.Models.Post>, IEnumerable<PostDTO>>(posts);

            return dtos;
        }
    }
}
