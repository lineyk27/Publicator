using System.Collections.Generic;
using System.Threading.Tasks;
using Publicator.Infrastructure.Models;

namespace Publicator.ApplicationCore.Contracts
{
    public interface IRoleService
    {
        public Task<Role> GetByNameAsync(string name);
        public Task<IEnumerable<Role>> GetAllAsync();
    }
}
