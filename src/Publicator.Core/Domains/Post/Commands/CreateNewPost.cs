using System;
using System.Collections.Generic;
using MediatR;

namespace Publicator.Core.Domains.Post.Commands
{
    public class CreateNewPost : User.Queries.CurrentUserId, IRequest<Infrastructure.Models.Post>
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public Guid CommunityId { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}
