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
        public void AddSubscriptionNewPost(Post post);
        public Task<int> GetCurrentRatingAsync(Post post);
        public void VoteAsync(Post post, bool up = false);
        public void AddCommentAsync(Post post, string text, Comment parentrepliedcomment);
        public void AddToBookmarkAsync(User user, Post post);
        public bool? VotedUpAsync(User user, Post post);
        public void CreateAsync(string name, string content, User creatoruser, Community community, IEnumerable<Tag> tags);
    }
}
