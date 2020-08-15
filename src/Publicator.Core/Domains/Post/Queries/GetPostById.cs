using System;
using MediatR;
using Publicator.Core.DTO;

namespace Publicator.Core.Domains.Post.Queries
{
    public class GetPostById : IRequest<PostDTO>
    {
        public Guid PostId { get; set; }
    }
}
