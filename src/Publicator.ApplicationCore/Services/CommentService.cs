using System;
using System.Linq;
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
        private IUserService _userService;
        public CommentService(IUnitOfWork unitOfWork, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }
        public async Task<Comment> GetByIdAsync(Guid id)
        {
            var comment = (await _unitOfWork
                .CommentRepository
                .GetAsync(x => x.Id == id, includeProperties:"User"))
                .FirstOrDefault();
            if (comment != null)
                return comment;
            throw new ResourceNotFoundException("Comment not found exception");
        }
        public async Task<IEnumerable<Comment>> GetByParentRepliedAsync(Comment parentrepliedcomment)
        {
            return (await _unitOfWork
                .CommentRepository
                .GetAsync(x => x.ParentRepliedCommentId == parentrepliedcomment.Id,
                    includeProperties:"ParentRepliedComment,RepliesComments.User"));
        }

        public async Task<IEnumerable<Comment>> GetByPostAsync(Post post)
        {
            return (await _unitOfWork
                .CommentRepository
                .GetAsync(x => x.PostId == post.Id && x.ParentRepliedCommentId == null,includeProperties: "RepliesComments.User,RepliesComments.RepliesComments,RepliesComments.RepliesComments.RepliesComments,User"))
                .OrderByDescending(x => x.CreationDate);
        }
        public async Task<Comment> AddToPost(Post post, string text, Comment parentreplied)
        {
            var user = await _userService.GetCurrentUserAsync();
            var id = Guid.NewGuid();
            _unitOfWork
                .CommentRepository
                .Insert(new Comment()
                {
                    Id = id,
                    PostId = post.Id,
                    UserId = user.Id,
                    Content = text,
                    CreationDate = DateTime.Now,
                    ParentRepliedCommentId = parentreplied?.Id
                });
            _unitOfWork.Save();
            return await GetByIdAsync(id);
        }
    }
}
