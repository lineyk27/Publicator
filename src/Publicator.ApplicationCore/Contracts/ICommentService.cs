using System.Collections.Generic;
using System.Threading.Tasks;
using Publicator.Infrastructure.Entities;

namespace Publicator.ApplicationCore.Contracts
{
    interface ICommentService
    {
        public Task<IEnumerable<Comment>> GetByPost(Post post);
    }
}
