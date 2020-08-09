using System;
using MediatR;

namespace Publicator.Core.Domains.Post.Queries
{
    class GetPostById : IRequest<Infrastructure.Models.Post>
    {
        public Guid PostId { get; set; }
    }
}
