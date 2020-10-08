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
using Publicator.Core.Services;

namespace Publicator.Core.Domains.Post.Queries
{
    class ListBookmarkedPostsHandler : IRequestHandler<ListBookmarkedPosts, IEnumerable<PostDTO>>
    {
        private readonly PublicatorDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        public ListBookmarkedPostsHandler(PublicatorDbContext context, IMapper mapper, IAuthService authService)
        {
            _context = context;
            _mapper = mapper;
            _authService = authService;
        }
        public async Task<IEnumerable<PostDTO>> Handle(
            ListBookmarkedPosts request, 
            CancellationToken cancellationToken
            )
        {
            var userId = _authService.GetCurrentUserId();
            var posts = await (from b in _context.Bookmarks.Include(x => x.Post)
                                where b.UserId.Equals((Guid)userId)
                                select b.Post
                                ).ToListAsync();

            var dtos = _mapper.Map<IEnumerable<Infrastructure.Models.Post>, IEnumerable<PostDTO>>(posts);

            return dtos;
        }
    }
}
