using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Publicator.Infrastructure.Entities;

namespace Publicator.ApplicationCore.Contracts
{
    interface ICommentService
    {
        public Task<Comment> GetById(Guid id); 
        public Task<IEnumerable<Comment>> GetByPostAsync(Post post);
        public Task<IEnumerable<Comment>> GetByParentReplied(Comment parentrepliedcomment);
    }
}
