using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Publicator.Core.DTO;
using Publicator.Infrastructure;

namespace Publicator.Core.Domains.Post.Queries
{
    class GetPostByIdHandler : IRequestHandler<GetPostById, PostDTO>
    {
        private readonly PublicatorDbContext _context;
        private readonly IMapper _mapper;
        public GetPostByIdHandler(PublicatorDbContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PostDTO> Handle(GetPostById request, CancellationToken cancellationToken)
        {
            var post = await (from p in _context.Posts
                                                .Include("PostTags.Tag")
                                                .Include(x => x.Community)
                                                .Include(x => x.CreatorUser)
                                                .Include(x => x.Bookmarks)
                                                .Include(x => x.Votes)
                              where p.Id.Equals(request.PostId)
                              select p
                ).FirstOrDefaultAsync();

            var dto = _mapper.Map<Infrastructure.Models.Post, PostDTO>(post);

            return dto;
        }
    }
}
