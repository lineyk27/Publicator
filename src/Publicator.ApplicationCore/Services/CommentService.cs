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
        public async Task<Comment> GetByIdAsync(Guid id)
        {
            var comment = await _unitOfWork
                .CommentRepository
                .GetByIdAsync(id);
            if (comment != null)
                return comment;
            throw new ResourceNotFoundException("Comment not found exception");
        }

        public async Task<IEnumerable<Comment>> GetByParentRepliedAsync(Comment parentrepliedcomment)
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
        public void AddToPost(Post post, User creatoruser, string text, Comment parentreplied)
        {
            // TODO add logic for prevent xss atack
            _unitOfWork
                .CommentRepository
                .Insert(new Comment()
                {
                    PostId = post.Id,
                    UserId = creatoruser.Id,
                    Content = text,
                    CreationDate = DateTime.Now,
                    ParentRepliedCommentId = parentreplied.Id
                });
        }
    }
}
