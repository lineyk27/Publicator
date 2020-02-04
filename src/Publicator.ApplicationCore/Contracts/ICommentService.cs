using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Publicator.Infrastructure.Entities;

namespace Publicator.ApplicationCore.Contracts
{
    public interface ICommentService
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public Task<Comment> GetByIdAsync(Guid id); 
        public Task<IEnumerable<Comment>> GetByPostAsync(Post post);
        public Task<IEnumerable<Comment>> GetByParentRepliedAsync(Comment parentrepliedcomment);
        public Task<Comment> AddToPost(Post post, string text, Comment parentreplied);
    }
}
