using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Publicator.Infrastructure.Entities;

namespace Publicator.ApplicationCore.Contracts
{
    interface ICommunityService
    {
        public Task<Community> GetById(Guid id);
        public Task<IEnumerable<Community>> GetAll();
        public Task<IEnumerable<Community>> GetBySearch(string query);
        public Task<IEnumerable<Community>> GetBySubscriberUser(User subscriberuser);
    }
}
