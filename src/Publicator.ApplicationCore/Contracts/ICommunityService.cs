﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Publicator.Infrastructure.Entities;

namespace Publicator.ApplicationCore.Contracts
{
    interface ICommunityService
    {
        public Task<Community> GetByIdAsync(Guid id);
        public Task<IEnumerable<Community>> GetAllAsync();
        public Task<IEnumerable<Community>> GetBySearchAsync(string query);
        public Task<IEnumerable<Community>> GetBySubscriberUserAsync(User subscriberuser);
    }
}