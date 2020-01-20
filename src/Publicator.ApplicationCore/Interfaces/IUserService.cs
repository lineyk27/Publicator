using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Publicator.Infrastructure.Entities;

namespace Publicator.ApplicationCore.Interfaces
{
    interface IUserService
    {
        public Task<User> GetCurrentUserAsync();
        public Task<User> GetByUsernameAsync(string username);
        public Task<User> GetByIdAsync(Guid id);
        public Task<IEnumerable<User>> GetByRoleAsync(Role role);
        public Task<IEnumerable<User>> GetByStateAsync(State role);
        public Task<IEnumerable<User>> GetBySearchAsync(string query);
        public Task<IEnumerable<User>> GetSubscriptionsAsync(User subscriberuser);
        public Task<IEnumerable<User>> GetSubscribersAsync(User subscriptionuser);
    }
}
