using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Publicator.Infrastructure;
using Publicator.Core.DTO;
using AutoMapper;
using Publicator.Core.Services;

namespace Publicator.Core.Domains.Post.Queries
{
    class ListPostsBySubscriptionHandler :
        IRequestHandler<ListPostsBySubscription, IEnumerable<PostDTO>>
    {
        private readonly PublicatorDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        public ListPostsBySubscriptionHandler(PublicatorDbContext context, IMapper mapper, IAuthService authService)
        { 
            _context = context;
            _mapper = mapper;
            _authService = authService;
        }
        public async Task<IEnumerable<PostDTO>> Handle(
            ListPostsBySubscription request, 
            CancellationToken cancellationToken
            )
        {
            var userId = _authService.GetCurrentUserId();
            var posts = (from p in _context.Posts
                         join s in _context.SubscriptionNewPosts on p.Id equals s.PostId
                         where s.UserId == userId
                         select p)
                         .Skip((request.Page-1) * request.PageSize)
                         .Take(request.PageSize);

            var dtos = _mapper.Map<
                IEnumerable<Infrastructure.Models.Post>, 
                IEnumerable<PostDTO>>(await posts.ToListAsync());

            return dtos;
        }
    }
}
