using System;
using System.Data;
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
    class ListPostsByCreatorUserHandler : IRequestHandler<ListPostsByCreatorUser, IEnumerable<PostDTO>>
    {
        private readonly PublicatorDbContext _context;
        private readonly IMapper _mapper;
        public ListPostsByCreatorUserHandler(PublicatorDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PostDTO>> Handle(
            ListPostsByCreatorUser request, 
            CancellationToken cancellationToken
            )
        {
            var posts = (from p in _context.Posts
                         where p.CreatorUser.Nickname.Equals(request.Username)
                         select p)
                         .Skip((request.Page - 1) * request.PageSize)
                         .Take(request.PageSize);

            var dtos = _mapper
                .Map<IEnumerable<Infrastructure.Models.Post>, IEnumerable<PostDTO>>(await posts.ToListAsync());
            
            return dtos;
        }
    }
}
