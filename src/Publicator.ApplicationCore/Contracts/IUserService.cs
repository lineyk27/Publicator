using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Publicator.Infrastructure.Entities;

namespace Publicator.ApplicationCore.Contracts
{
    interface IUserService
    {
        public Task<User> GetCurrentUserAsync();
        public Task<User> GetByUsernameAsync(string username);
        public Task<User> GetByIdAsync(Guid id);
        public Task<IEnumerable<User>> GetByRoleAsync(Role role);
        public Task<IEnumerable<User>> GetByStateAsync(State state);
        public Task<IEnumerable<User>> GetBySearchAsync(string query);
        public Task<User> GetByPost(Post post);
        public Task<IEnumerable<User>> GetSubscriptionsAsync(User subscriberuser);
        public Task<IEnumerable<User>> GetSubscribersAsync(User subscriptionuser);
        public Task<User> LoginAsync(string login, string password);
        public void RegisterAsync(string username, string email, string password);
        public void ConfirmAccountAsync(User user, string token);
    }
}
