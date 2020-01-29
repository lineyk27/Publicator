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
        private int _page;
        private int _pageSize;
        public int Page
        {
            get
            {
                return _page;
            }
            set
            {
                if (value > 0)
                    _page = value;
            }
        }
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                if (value > 0)
                    _pageSize = value;
            }
        }
        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            Page = 1;
            PageSize = 10;
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
        public int GetStartPage()
        {
            return (Page - 1) * PageSize;
        }
        public async Task<IEnumerable<Comment>> GetByParentRepliedAsync(Comment parentrepliedcomment)
        {
            return (await _unitOfWork
                .CommentRepository
                .GetAsync(x => x.ParentRepliedCommentId == parentrepliedcomment.Id,
                    includeProperties:"ParentRepliedComment,RepliesComments.User"))
                .Skip(GetStartPage())
                .Take(PageSize);
        }

        public async Task<IEnumerable<Comment>> GetByPostAsync(Post post)
        {
            return (await _unitOfWork
                .CommentRepository
                .GetAsync(x => x.PostId == post.Id && x.ParentRepliedCommentId == null,includeProperties: "RepliesComments.User"))
                .Skip(GetStartPage())
                .Take(PageSize)
                .OrderByDescending(x => x.CreationDate);
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
