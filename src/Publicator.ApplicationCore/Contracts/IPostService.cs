using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Publicator.Infrastructure.Entities;

namespace Publicator.ApplicationCore.Contracts
{
    interface IPostService
    {
        public Task<IEnumerable<Post>> GetAllAsync();
        public Task<IEnumerable<Post>> GetByTagAsync(Tag tag);
        public Task<Post> GetByIdAsync(Guid postid);
        public Task<IEnumerable<Post>> GetBySubscriptionAsync(User user);
        public Task<IEnumerable<Post>> GetByCreatorAsync(User creatoruser);
        public Task<IEnumerable<Post>> GetBookmarks(User user);
        public Task<IEnumerable<Post>> GetByCommunity(Community community);
        public Task<IEnumerable<Post>> GetVotedByCreatorAsync(User creatorvoteuser);
        public void AddSubscriptionNewPostAsync(Post post);
        public Task<int> CalcCurrentRatingAsync(Post post);
        public Task<Vote> VoteAsync(Post post, bool up = false);
        public Task<bool> AddToBookmarkAsync(Post post);
        public Task<Vote> CurrentVoteAsync(User creatoruser, Post post);
        public void CreateAsync(string name, string content, Community community, IEnumerable<Tag> tags);
    }
}
