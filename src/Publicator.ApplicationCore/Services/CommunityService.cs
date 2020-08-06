using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Publicator.ApplicationCore.Contracts;
using Publicator.ApplicationCore.Exceptions;
using Publicator.Infrastructure.Models;
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

        public void ChangePicture(Community community, string url)
        {
            community.PictureName = url;
            _unitOfWork.CommunityRepository.Update(community);
            _unitOfWork.Save();
        }

        public async Task<Guid> CreateNewAsync(string name, string description, string imageUrl)
        {
            var current = (await _unitOfWork
                .CommunityRepository
                .GetAsync(x => x.Name.ToLower() == name.ToLower()))
                .FirstOrDefault();
            if(current == null)
            {
                var newcomm = new Community()
                {
                    Name = name,
                    Description = description,
                    PictureName = imageUrl,
                    CreationDate = DateTime.Now
                };
                _unitOfWork.CommunityRepository.Insert(newcomm);
                _unitOfWork.Save();
                return newcomm.Id;
            }
            else
            {
                throw new ResourceException("Community with the name is already exist");
            }
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
            return (await _unitOfWork
                .CommunityRepository
                .GetAsync(x => x.Posts.Any(y => y.Id == post.Id),includeProperties: "Posts"))
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
