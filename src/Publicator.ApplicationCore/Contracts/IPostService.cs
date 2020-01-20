using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Publicator.Infrastructure.Entities;

namespace Publicator.ApplicationCore.Contracts
{
    interface IPostService
    {
        public Task<IEnumerable<Post>> GetAllAsync();
        public Task<IEnumerable<Post>> GetByTagAsync(string tag);
        public Task<Post> GetByIdAsync(Guid postid);
        public Task<IEnumerable<Post>> GetBySubscriptionAsync(User user);
        public Task<IEnumerable<Post>> GetByCreatorAsync(User creatoruser);
        public Task<IEnumerable<Post>> GetByCommunity(Community community);
        public Task<IEnumerable<Post>> GetVotedByCreatorAsync(User creatorvoteuser);
        public Task<int> GetCurrentRatingAsync(Guid postid);
        public void VoteAsync(Guid postid, bool up = false);
        public void AddCommentAsync(Guid postid, string text, Guid? parentrepliedcommentid=null);
        public void AddToBookmarkAsync(string username, Guid postid);
    }
}
