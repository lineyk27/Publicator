using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Publicator.Infrastructure.Models;
using Publicator.Infrastructure;

namespace Publicator.Core.Domains.Post.Queries
{
    class GetPostByIdHandler : IRequestHandler<GetPostById, Infrastructure.Models.Post>
    {
        private readonly PublicatorDbContext _context;
        public GetPostByIdHandler(PublicatorDbContext context) => _context = context;
        public async Task<Infrastructure.Models.Post> Handle(GetPostById request, CancellationToken cancellationToken)
        {
            var post = (from p in _context.Posts
                        where p.Id == request.PostId
                        select p
                ).SingleOrDefault();

            return await Task.FromResult(post);
        }
    }
}
