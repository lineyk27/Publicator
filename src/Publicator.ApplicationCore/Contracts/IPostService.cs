using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Publicator.Infrastructure.Entities;

namespace Publicator.ApplicationCore.Contracts
{
    public interface IPostService
    {
        public int PageSize { get; set; }
        public int Page { get; set; }
        public HotPeriod Period { get; set; }
        public Task<IEnumerable<Post>> GetNewAsync();
        public Task<IEnumerable<Post>> GetByTagAsync(Tag tag);
        public Task<Post> GetByIdAsync(Guid postid);
        public Task<IEnumerable<Post>> GetBySubscriptionAsync();
        public Task<IEnumerable<Post>> GetByCreatorAsync(User creatoruser);
        public Task<IEnumerable<Post>> GetBookmarks();
        public Task<IEnumerable<Post>> GetByCommunity(Community community);
        public Task<IEnumerable<Post>> GetVotedByCreatorAsync(User creatorvoteuser);
        public Task AddSubscriptionNewPostAsync(Post post);
        public Task<IEnumerable<Post>> GetHotAsync();
        public Task<IEnumerable<Post>> GetBySearchAsync(string query, DateTime? startDate, DateTime? endDate, int? minRating, Community community, User creatorUser);
        public Task<int> CalcCurrentRatingAsync(Post post);
        public Task<Vote> VoteAsync(Post post, bool up = false);
        public Task<bool> AddToBookmarkAsync(Post post);
        public Task<Vote> CurrentVoteAsync(Post post);
        public Task<Post> CreateAsync(string name, string content, Community community, IEnumerable<Tag> tags);
    }
}
