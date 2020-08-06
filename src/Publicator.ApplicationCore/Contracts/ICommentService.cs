using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Publicator.Infrastructure.Models;

namespace Publicator.ApplicationCore.Contracts
{
    public interface ICommentService
    {
        public Task<Comment> GetByIdAsync(Guid id); 
        public Task<IEnumerable<Comment>> GetByPostAsync(Post post);
        public Task<IEnumerable<Comment>> GetByParentRepliedAsync(Comment parentrepliedcomment);
        public Task<Comment> AddToPost(Post post, string text, Comment parentreplied);
    }
}
