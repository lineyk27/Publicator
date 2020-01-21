using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Publicator.Infrastructure.Entities;

namespace Publicator.ApplicationCore.Contracts
{
    interface ICommentService
    {
        public Task<Comment> GetByIdAsync(Guid id); 
        public Task<IEnumerable<Comment>> GetByPostAsync(Post post);
        public Task<IEnumerable<Comment>> GetByParentRepliedAsync(Comment parentrepliedcomment);
        public void AddToPost(Post post, User creatoruser, string text, Comment parentreplied);
    }
}
