using System;
using MediatR;

namespace Publicator.Core.Domains.Post.Commands
{
    class CreateNewPost : User.Queries.CurrentUserId, IRequest<Infrastructure.Models.Post>
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public Guid CommunityId { get; set; }
    }
}
