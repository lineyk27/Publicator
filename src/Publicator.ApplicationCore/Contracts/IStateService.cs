using System.Collections.Generic;
using System.Threading.Tasks;
using Publicator.Infrastructure.Entities;

namespace Publicator.ApplicationCore.Contracts
{
    interface IStateService
    {
        public Task<State> GetByNameAsync(string name);
        public Task<IEnumerable<State>> GetAllAsync();
    }
}
