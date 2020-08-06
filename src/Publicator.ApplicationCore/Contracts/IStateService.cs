using System.Collections.Generic;
using System.Threading.Tasks;
using Publicator.Infrastructure.Models;

namespace Publicator.ApplicationCore.Contracts
{
    public interface IStateService
    {
        public Task<State> GetByNameAsync(string name);
        public Task<IEnumerable<State>> GetAllAsync();
    }
}
