using System.Collections.Generic;
using Publicator.ApplicationCore.DTO;
using Publicator.Infrastructure.Models;

namespace Publicator.ApplicationCore.Contracts
{
    public interface IAggregationService
    {
        public IEnumerable<PostDTO> AggregateWithBookmarkVote(IEnumerable<Post> posts, User user);        
        public PostDTO AggregateWithBookmarkVote(Post post,User user);
    }
}
