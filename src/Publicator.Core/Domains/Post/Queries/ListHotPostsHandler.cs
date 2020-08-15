using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Publicator.Infrastructure.Models;
using Publicator.Infrastructure;
using Publicator.Core.Domains.Post.Queries;
using Publicator.Core.DTO;
using AutoMapper;

namespace Publicator.Core.Handlers
{
    class ListHotPostsHandler : IRequestHandler<ListHotPosts, IEnumerable<PostDTO>>
    {
        private readonly PublicatorDbContext _context;
        private readonly IMapper _mapper;
        public ListHotPostsHandler(PublicatorDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PostDTO>> Handle(ListHotPosts request, CancellationToken cancellationToken)
        {
            var startDate = DateTime.Now.AddDays(-(int)request.Period);
            
            var posts = (from p in _context.Posts
                         where p.CreationDate >= startDate
                         orderby p.CurrentRating
                         select p)
                         .Skip(request.PageSize * (request.Page-1))
                         .Take(request.PageSize);

            var dtos = _mapper.Map<IEnumerable<Post>, IEnumerable<PostDTO>>(await posts.ToListAsync());

            return dtos;
        }
    }
}
