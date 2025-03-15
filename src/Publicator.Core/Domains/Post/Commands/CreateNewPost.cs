using MediatR;
using Publicator.Core.DTO;
using System;
using System.Collections.Generic;

namespace Publicator.Core.Domains.Post.Commands
{
    public class CreateNewPost : IRequest<PostDTO>
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public Guid CommunityId { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}
