using System;
using MediatR;
using Publicator.Core.Domains.User.Queries;

namespace Publicator.Core.Domains.Post.Commands
{
    public class AddPostToBookmarks : IRequest<BookmarkResult>
    {
        public Guid PostId { get; set; }
    }
}
