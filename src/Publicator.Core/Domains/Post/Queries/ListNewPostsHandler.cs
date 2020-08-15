using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Publicator.Core.Domains.Post.Queries;
using Publicator.Core.DTO;
using Publicator.Infrastructure;
using Publicator.Infrastructure.Models;

namespace Publicator.Core.Handlers
{
    class ListNewPostsHandler : IRequestHandler<ListNewPosts, IEnumerable<PostDTO>>
    {
        private readonly PublicatorDbContext _context;
        private readonly IMapper _mapper;
        public ListNewPostsHandler(PublicatorDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PostDTO>> Handle(
            ListNewPosts request, 
            CancellationToken cancellationToken
            )
        {
            var posts = (from p in _context.Posts
                         orderby p.CreationDate
                         select p
                         ).Skip(request.PageSize * (request.Page - 1))
                         .Take(request.PageSize);

            var dtos = _mapper.Map<IEnumerable<Post>, IEnumerable<PostDTO>>(await posts.ToListAsync());

            return dtos;
        }
    }
}
