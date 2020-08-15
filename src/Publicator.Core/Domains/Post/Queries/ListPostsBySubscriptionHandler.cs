using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Publicator.Infrastructure;
using Publicator.Core.DTO;
using AutoMapper;

namespace Publicator.Core.Domains.Post.Queries
{
    class ListPostsBySubscriptionHandler :
        IRequestHandler<ListPostsBySubscription, IEnumerable<PostDTO>>
    {
        private readonly PublicatorDbContext _context;
        private readonly IMapper _mapper;
        public ListPostsBySubscriptionHandler(PublicatorDbContext context, IMapper mapper)
        { 
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PostDTO>> Handle(
            ListPostsBySubscription request, 
            CancellationToken cancellationToken
            )
        {
            var posts = (from p in _context.Posts
                         join s in _context.SubscriptionNewPosts on p.Id equals s.PostId
                         where s.UserId == request.UserId
                         select p);

            var dtos = _mapper.Map<
                IEnumerable<Infrastructure.Models.Post>, 
                IEnumerable<PostDTO>>(posts);

            return await Task.Run(() => dtos.ToList());
        }
    }
}
