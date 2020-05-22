using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Publicator.Infrastructure.Entities;

namespace Publicator.ApplicationCore.Contracts
{
    public interface IUserService
    {
        public Task<User> GetCurrentUserAsync();
        public Task<User> GetByUsernameAsync(string username);
        public Task<User> GetByEmailAsync(string email);
        public Task<User> GetByIdAsync(Guid id);
        public Task<IEnumerable<User>> GetByRoleAsync(Role role);
        public Task<IEnumerable<User>> GetByStateAsync(State state);
        public Task<IEnumerable<User>> GetBySearchAsync(string query);
        public Task<User> GetByPostAsync(Post post);
        public Task<IEnumerable<User>> GetSubscriptionsAsync(User subscriberuser);
        public Task<IEnumerable<User>> GetSubscribersAsync(User subscriptionuser);
        public Task<User> LoginAsync(string login, string password);
        public Task RegisterAsync(string username, string email, string password);
        public bool ConfirmAccount(User user, string token);
        public Task<bool> MakeSubscription(User subscription);
        public void ChangeUserPicture(string url);
        public Task<User> TryGetCurrentAsync();
        public Task<bool> GetCurrentSubscriptionAsync(User user);
    }
}
