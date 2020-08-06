using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Publicator.ApplicationCore.Contracts;
using Publicator.ApplicationCore.Exceptions;
using Publicator.Infrastructure.Models;
using Publicator.Infrastructure.Interfaces;

namespace Publicator.ApplicationCore.Services
{
    class StateService : IStateService
    {
        IUnitOfWork _unitOfWork;
        public StateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<State>> GetAllAsync()
        {
            return await _unitOfWork
                .StateRepository
                .GetAsync();
        }

        public async Task<State> GetByNameAsync(string name)
        {
            var state = (await _unitOfWork
                .StateRepository
                .GetAsync(x => x.Name.ToLower() == name.ToLower()))
                .FirstOrDefault();
            if (state != null)
                return state;
            throw new ResourceNotFoundException("State not found");
        }
    }
}
