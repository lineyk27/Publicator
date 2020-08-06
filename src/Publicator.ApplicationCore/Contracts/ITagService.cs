using System.Threading.Tasks;
using System.Collections.Generic;
using Publicator.Infrastructure.Models;

namespace Publicator.ApplicationCore.Contracts
{
    public interface ITagService
    {
        public Task<Tag> GetByNameAsync(string name);
        public Task<IEnumerable<Tag>> GetByPostAsync(Post post);
        public Task<Tag> CreateAsync(string name);
        public Task<IEnumerable<Tag>> CreateAsync(IEnumerable<string> names);
    }
}
