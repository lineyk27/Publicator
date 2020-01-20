using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Publicator.ApplicationCore.Contracts;
using Publicator.ApplicationCore.Exceptions;
using Publicator.Infrastructure.Entities;
using Publicator.Infrastructure.Interfaces;

namespace Publicator.ApplicationCore.Services
{
    class CommentService : ICommentService
    {
        private IUnitOfWork _unitOfWork;
        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Comment> GetById(Guid id)
        {
            var comment = await _unitOfWork
                .CommentRepository
                .GetByIdAsync(id);
            if (comment != null)
                return comment;
            throw new ResourceNotFoundException("Comment not found exception");
        }

        public async Task<IEnumerable<Comment>> GetByParentReplied(Comment parentrepliedcomment)
        {
            return await _unitOfWork
                .CommentRepository
                .GetAsync(x => x.ParentRepliedCommentId == parentrepliedcomment.Id,
                    includeProperties:"ParentRepliedComment");
        }

        public async Task<IEnumerable<Comment>> GetByPostAsync(Post post)
        {
            return await _unitOfWork
                .CommentRepository
                .GetAsync(x => x.PostId == post.Id,includeProperties:"ParentRepliedComment");
        }
    }
}
