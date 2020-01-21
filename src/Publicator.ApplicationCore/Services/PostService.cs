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
    public class PostService : IPostService
    {
        private IUnitOfWork _unitOfWork;
        private IUserService _userService;
        private ITagService _tagService;
        private ICommunityService _communityService;
        public PostService(
            IUnitOfWork unitOfWork, 
            IUserService userService, 
            ITagService tagService,
            ICommunityService communityService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _tagService = tagService;
            _communityService = communityService;
        }

        public async void AddSubscriptionNewPostAsync(Post post)
        {
            var currentuser = await _userService.GetCurrentUserAsync();
            //var tags = await _tagService.GetByPostAsync(post);
            var creator = await _userService.GetByPostAsync(post);
            var community = await _communityService.GetByPostAsync(post);
            var usersubscribers = (await _unitOfWork
                .UserSubscriptionRepository
                .GetAsync(x => x.SubscriptionUserId == currentuser.Id,includeProperties:"SubscriberUser"))
                .Select(x => x.SubscriberUser);
            var communitysubscribers = (await _unitOfWork
                .UserCommunityRepository
                .GetAsync(x => x.CommunityId == community.Id,includeProperties:"User"))
                .Select(x => x.User);
            foreach (var i in usersubscribers)
            {
                await AddSingleSubscriptionNewPostAsync(post, i, creator, null);
            }
            foreach (var i in communitysubscribers)
            {
                await AddSingleSubscriptionNewPostAsync(post, i, null, community);
            }
            // TODO add tags subscribe logic(or no)
        }

        public async Task AddSingleSubscriptionNewPostAsync(Post post,
            User subscriberuser,
            User subscriptionuser,
            Community subscriptioncommunity)
        {
            var currentsubscription = (await _unitOfWork
                .SubscriptionNewPostRepository
                .GetAsync(x => x.UserId == subscriberuser.Id && x.PostId == post.Id))
                .FirstOrDefault();
            if(currentsubscription != null)
            {
                currentsubscription.SubscriptionCommunityId = (Guid)subscriptioncommunity?.Id;
                currentsubscription.SubscriptionUserId = (Guid)subscriptionuser?.Id;
                _unitOfWork
                    .SubscriptionNewPostRepository
                    .Update(currentsubscription);                
            }
            else
            {
                var newpost = new SubscriptionNewPost()
                {
                    PostId = post.Id,
                    UserId = subscriberuser.Id
                };
                newpost.SubscriptionCommunityId = (Guid)subscriptioncommunity?.Id;
                newpost.SubscriptionUserId = (Guid)subscriptionuser?.Id;
                _unitOfWork
                    .SubscriptionNewPostRepository
                    .Insert(newpost);
            }
            _unitOfWork.Save();
        }

        public async Task<bool> AddToBookmarkAsync(Post post)
        {
            var user = await _userService.GetCurrentUserAsync();
            var currentbookmark = (await _unitOfWork
                .BookmarkRepository
                .GetAsync(x => x.UserId == user.Id && x.PostId == post.Id))
                .FirstOrDefault();
            if(currentbookmark != null)
            {
                var newbookmark= new Bookmark()
                {
                    UserId = user.Id,
                    PostId = post.Id,
                    CreationDate = DateTime.Now
                };
                _unitOfWork.BookmarkRepository.Insert(newbookmark);
                _unitOfWork.Save();
                return true;
            }
            return false;
        }
        public async Task<bool> RemoveFromBookmarkAsync(Post post)
        {
            var user = await _userService.GetCurrentUserAsync();
            var currentbookmark = (await _unitOfWork
                .BookmarkRepository
                .GetAsync(x => x.UserId == user.Id && x.PostId == post.Id))
                .FirstOrDefault();
            if (currentbookmark != null)
            {
                _unitOfWork.BookmarkRepository.Delete(currentbookmark);
                _unitOfWork.Save();
                return true;
            }
            return false;
        }

        public async Task<bool> HasBookmarkAsync(Post post)
        {
            var user = await _userService.GetCurrentUserAsync();
            return (await _unitOfWork
                .BookmarkRepository
                .GetAsync(x => x.PostId == post.Id && x.UserId == user.Id))
                .FirstOrDefault() != null;
        }

        public async void CreateAsync(string name, string content, Community community, IEnumerable<Tag> tags)
        {
            var user = await _userService.GetCurrentUserAsync();
            var post = new Post()
            {
                Name = name,
                Content = content,
                CommunityId = community.Id,
                CreationDate = DateTime.Now,
                CreatorUserId = user.Id
            };
            _unitOfWork.PostRepository.Insert(post);
            AddTagsToPostAsync(post, tags);
        }

        public void AddTagsToPostAsync(Post post, IEnumerable<Tag> tags)
        {
            foreach(var i in tags)
            {
                var tag = _tagService.Create(i.Name);
                _unitOfWork.PostTagRepository.Insert(new PostTag()
                {
                    PostId = post.Id,
                    TagId = i.Id
                });
            }
            _unitOfWork.Save();
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _unitOfWork
                .PostRepository
                .GetAsync(includeProperties: "Bookmarks,Votes");
        }

        public async Task<IEnumerable<Post>> GetBookmarks(User user)
        {
            return (await _unitOfWork
                .BookmarkRepository
                .GetAsync(x => x.UserId == user.Id,includeProperties:"Post"))
                .Select(x => x.Post);
        }

        public async Task<IEnumerable<Post>> GetByCommunity(Community community)
        {
            return (await _unitOfWork
                .CommunityRepository
                .GetAsync(x => x.Id == community.Id, includeProperties: "Posts"))
                .FirstOrDefault()
                .Posts;
        }

        public async Task<IEnumerable<Post>> GetByCreatorAsync(User creatoruser)
        {
            return await _unitOfWork
                .PostRepository
                .GetAsync(x => x.CreatorUserId == creatoruser.Id);
        }

        public async Task<Post> GetByIdAsync(Guid postid)
        {
            var post = await _unitOfWork.PostRepository.GetByIdAsync(postid);
            if (post == null)
                throw new ResourceNotFoundException("Post not found");
            return post;
        }

        public async Task<IEnumerable<Post>> GetBySubscriptionAsync(User user)
        {
            return (await _unitOfWork
                .UserSubscriptionRepository
                .GetByIdAsync(user.Id))
                .SubscriptionUser
                .Posts;
        }

        public async Task<IEnumerable<Post>> GetByTagAsync(Tag tag)
        {
            return (await _unitOfWork
                .PostTagRepository
                .GetAsync(x => x.TagId == tag.Id,includeProperties:"Post"))
                .Select(x => x.Post);
        }

        public async Task<int> CalcCurrentRatingAsync(Post post)
        {
            var currentrating = (await _unitOfWork
                .VoteRepository
                .GetAsync(x => x.PostId == post.Id))
                .Select(x => x.Up ? 1 : -1)
                .Sum();
            post.CurrentRating = currentrating;
            _unitOfWork
                .PostRepository
                .Update(post);
            _unitOfWork.Save();
            return currentrating;
        }

        public async Task<IEnumerable<Post>> GetVotedByCreatorAsync(User creatorvoteuser)
        {
            return (await _unitOfWork
                .PostRepository
                .GetAsync(x => x.Votes.Any(t => t.UserId == creatorvoteuser.Id), includeProperties:"Votes"));
        }

        public async Task<Vote> VoteAsync(Post post, bool up = false)
        {
            var user = await _userService.GetCurrentUserAsync();
            var currentvote = (await _unitOfWork
                .VoteRepository
                .GetAsync(x => x.PostId == post.Id && x.UserId == user.Id))
                .FirstOrDefault();
            if(currentvote == null)
            {
                var newvote = new Vote()
                {
                    PostId = post.Id,
                    UserId = user.Id,
                    Up = up
                };
                _unitOfWork.VoteRepository.Insert(newvote);
                return newvote;
            }
            else
            {
                if(currentvote.Up == up)
                {
                    _unitOfWork.VoteRepository.Delete(currentvote);
                }
                else
                {
                    currentvote.Up = up;
                    _unitOfWork.VoteRepository.Update(currentvote);
                }
                _unitOfWork.Save();
                return currentvote;
            }
        }

        public async Task<Vote> CurrentVoteAsync(User user, Post post)
        {
            return (await _unitOfWork
                .VoteRepository
                .GetAsync(x => x.UserId == user.Id && x.PostId == post.Id))
                .FirstOrDefault();
        }
    }
}
