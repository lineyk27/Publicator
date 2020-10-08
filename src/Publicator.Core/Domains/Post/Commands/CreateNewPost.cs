using System;
using System.Collections.Generic;
using MediatR;
using Publicator.Core.Domains.User.Queries;
using Publicator.Core.DTO;

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
