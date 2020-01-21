﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Publicator.ApplicationCore.Contracts;
using Publicator.ApplicationCore.Exceptions;
using Publicator.Infrastructure.Entities;
using Publicator.Infrastructure.Interfaces;

namespace Publicator.ApplicationCore.Services
{
    class CommunityService : ICommunityService
    {
        private IUnitOfWork _unitOfWork;
        public CommunityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Community>> GetAllAsync()
        {
            return await _unitOfWork
                .CommunityRepository
                .GetAsync();
        }
        public async Task<Community> GetByIdAsync(Guid id)
        {
            var community = await _unitOfWork
                .CommunityRepository
                .GetByIdAsync(id);
            if (community != null)
                return community;
            throw new ResourceNotFoundException("Community not found");
        }

        public async Task<Community> GetByPostAsync(Post post)
        {
            // TODO need to be reconsidered
            return (await _unitOfWork
                .CommunityRepository
                .GetAsync(x => x.Posts.Contains(post)))
                .FirstOrDefault();
        }

        public async Task<IEnumerable<Community>> GetBySearchAsync(string query)
        {
            return await _unitOfWork
                .CommunityRepository
                .GetAsync(x => x.Name.ToLower().Contains(query.ToLower()));
        }

        public async Task<IEnumerable<Community>> GetBySubscriberUserAsync(User subscriberuser)
        {
            return (await _unitOfWork
                .UserCommunityRepository
                .GetAsync(x => x.UserId == subscriberuser.Id, includeProperties: "Community"))
                .Select(x => x.Community);
        }
    }
}
