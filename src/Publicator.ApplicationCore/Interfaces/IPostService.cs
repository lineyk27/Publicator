using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Publicator.Infrastructure.Entities;

namespace Publicator.ApplicationCore.Interfaces
{
    interface IPostService
    {
        public Task<IEnumerable<Post>> GetAllAsync();
        public Task<IEnumerable<Post>> GetByTagAsync(string tag);
        public Task<Post> GetByIdAsync(Guid postid);
        public Task<IEnumerable<Post>> GetBySubscriptionAsync(Guid userid);
        public Task<IEnumerable<Post>> GetByCreatorAsync(User creatoruser);
        public Task<IEnumerable<Post>> GetVotedByCreatorAsync(User creatorvoteuser);
    }
}
