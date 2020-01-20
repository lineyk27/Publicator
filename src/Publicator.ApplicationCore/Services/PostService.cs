using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Publicator.ApplicationCore.Contracts;
using Publicator.ApplicationCore.Exceptions;
using Publicator.Infrastructure.Entities;
using Publicator.Infrastructure.Interfaces;


namespace Publicator.ApplicationCore.Services
{
    class PostService : IPostService
    {
        public void AddCommentAsync(Post post, string text, Comment parentrepliedcomment)
        {
            throw new NotImplementedException();
        }

        public void AddSubscriptionNewPost(Post post)
        {
            throw new NotImplementedException();
        }

        public void AddToBookmarkAsync(User user, Post post)
        {
            throw new NotImplementedException();
        }

        public void CreateAsync(string name, string content, User creatoruser, Community community, IEnumerable<Tag> tags)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Post>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Post>> GetBookmarks(User user)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Post>> GetByCommunity(Community community)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Post>> GetByCreatorAsync(User creatoruser)
        {
            throw new NotImplementedException();
        }

        public Task<Post> GetByIdAsync(Guid postid)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Post>> GetBySubscriptionAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Post>> GetByTagAsync(Tag tag)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCurrentRatingAsync(Post post)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Post>> GetVotedByCreatorAsync(User creatorvoteuser)
        {
            throw new NotImplementedException();
        }

        public void VoteAsync(Post post, bool up = false)
        {
            throw new NotImplementedException();
        }

        public bool? VotedUpAsync(User user, Post post)
        {
            throw new NotImplementedException();
        }
    }
}
