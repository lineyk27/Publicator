﻿using System.Collections.Generic;
using Publicator.ApplicationCore.DTO;
using Publicator.Infrastructure.Entities;

namespace Publicator.ApplicationCore.Contracts
{
    public interface IAggregationService
    {
        public IEnumerable<PostDTO> AggregateWithBookmarkVote(IEnumerable<Post> posts, User user);
    }
}