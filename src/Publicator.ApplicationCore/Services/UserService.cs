using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Publicator.ApplicationCore.Contracts;
using Publicator.ApplicationCore.Exceptions;
using Publicator.Infrastructure.Entities;
using Publicator.Infrastructure.Interfaces;

namespace Publicator.ApplicationCore.Services
{
    class UserService : IUserService
    {
        public void ConfirmAccountAsync(User user, string token)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByPostAsync(Post post)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetByRoleAsync(Role role)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetBySearchAsync(string query)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetByStateAsync(State state)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetCurrentUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetSubscribersAsync(User subscriptionuser)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetSubscriptionsAsync(User subscriberuser)
        {
            throw new NotImplementedException();
        }

        public Task<User> LoginAsync(string login, string password)
        {
            throw new NotImplementedException();
        }

        public void RegisterAsync(string username, string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
