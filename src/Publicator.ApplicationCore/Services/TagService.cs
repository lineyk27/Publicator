using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Publicator.ApplicationCore.Contracts;
using Publicator.ApplicationCore.Exceptions;
using Publicator.Infrastructure.Entities;
using Publicator.Infrastructure.Interfaces;

namespace Publicator.ApplicationCore.Services
{
    class TagService : ITagService
    {
        private IUnitOfWork _unitOfWork;
        public TagService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Tag> GetByNameAsync(string name)
        {
            var tag = (await _unitOfWork
                .TagRepository
                .GetAsync(x => x.Name.ToLower() == name.ToLower()))
                .FirstOrDefault();
            return tag;
        }

        public async Task<IEnumerable<Tag>> GetByPostAsync(Post post)
        {
            return (await _unitOfWork
                .PostTagRepository
                .GetAsync(x => x.PostId == post.Id))
                .Select(x => x.Tag);
        }

        public async Task<Tag> CreateAsync(string name)
        {
            var found = await GetByNameAsync(name);
            if(found == null)
            {
                var tag = new Tag()
                {
                    Id = Guid.NewGuid(),
                    Name = name
                };
                _unitOfWork.TagRepository.Insert(tag);
                _unitOfWork.Save();
                return tag;
            }
            return found;
        }
    }
}
