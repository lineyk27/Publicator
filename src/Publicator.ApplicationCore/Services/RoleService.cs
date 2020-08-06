using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Publicator.ApplicationCore.Contracts;
using Publicator.ApplicationCore.Exceptions;
using Publicator.Infrastructure.Models;
using Publicator.Infrastructure.Interfaces;

namespace Publicator.ApplicationCore.Services
{
    public class RoleService : IRoleService
    {
        private IUnitOfWork _unitOfWork;
        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _unitOfWork.RoleRepository.GetAsync();
        }

        public async Task<Role> GetByNameAsync(string name)
        {
            var role = (await _unitOfWork
                .RoleRepository
                .GetAsync(x => x.Name.ToLower() == name.ToLower()))
                .FirstOrDefault();
            if(role != null)
            {
                return role;
            }
            throw new ResourceNotFoundException("Role not found");
        }
    }
}
