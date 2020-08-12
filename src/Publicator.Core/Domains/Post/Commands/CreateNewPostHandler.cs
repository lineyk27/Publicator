using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Publicator.Infrastructure;

namespace Publicator.Core.Domains.Post.Commands
{
    class CreateNewPostHandler : IRequestHandler<CreateNewPost, Infrastructure.Models.Post>
    {
        private readonly PublicatorDbContext _context;
        public CreateNewPostHandler(PublicatorDbContext context) => _context = context;
        public async Task<Infrastructure.Models.Post> Handle(
            CreateNewPost request,
            CancellationToken cancellationToken)
        {
            var post = new Infrastructure.Models.Post()
            {
                Name = request.Name,
                Content = request.Content,
                CommunityId = request.CommunityId,
                CreationDate = DateTime.Now,
                CreatorUserId = (Guid)request.UserId
            };
            
            _context.Posts.Add(post);

            await _context.SaveChangesAsync(cancellationToken);

            AddTagsToPost(request.Tags, post.Id);

            return post;
        }
        private void AddTagsToPost(IEnumerable<string> tags, Guid postId)
        {
            foreach(string tag in tags)
            {
                var foundTag = (from t in _context.Tags
                                where t.Name.Equals(tag)
                                select t)
                                .FirstOrDefault();

                if (foundTag == null)
                {
                    foundTag = new Infrastructure.Models.Tag()
                    {
                        Name = tag
                    };
                    _context.Tags.Add(foundTag);
                }

                _context.PostTags.Add(new Infrastructure.Models.PostTag()
                {
                    TagId = foundTag.Id,
                    PostId = postId
                });
            }
            _context.SaveChanges();
        }
    }
}
