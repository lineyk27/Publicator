using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Publicator.Core.DTO;
using Publicator.Infrastructure;

namespace Publicator.Core.Domains.Post.Queries
{
    class GetPostByIdHandler : IRequestHandler<GetPostById, PostDTO>
    {
        private readonly PublicatorDbContext _context;
        private readonly IMapper _mapper;
        public GetPostByIdHandler(PublicatorDbContext context) => _context = context;
        public async Task<PostDTO> Handle(GetPostById request, CancellationToken cancellationToken)
        {
            var post = (from p in _context.Posts
                        where p.Id == request.PostId
                        select p
                ).SingleOrDefault();

            var dto = _mapper.Map<Infrastructure.Models.Post, PostDTO>(post);

            return await Task.FromResult(dto);
        }
    }
}
