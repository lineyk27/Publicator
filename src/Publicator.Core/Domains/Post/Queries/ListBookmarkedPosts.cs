using System;
using System.Collections.Generic;
using MediatR;
using Publicator.Core.Domains.User.Queries;
using Publicator.Core.DTO;

namespace Publicator.Core.Domains.Post.Queries
{
    public class ListBookmarkedPosts : LoggedInUser, IRequest<IEnumerable<PostDTO>>
    {
    }
}
