using System.Threading.Tasks;
using System.Collections.Generic;
using Publicator.Infrastructure.Entities;

namespace Publicator.ApplicationCore.Contracts
{
    interface ITagService
    {
        public Task<Tag> GetByNameAsync(string name);
        public Task<IEnumerable<Tag>> GetByPostAsync(Post post);
    }
}
